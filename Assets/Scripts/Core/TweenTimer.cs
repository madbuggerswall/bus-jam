using System.Collections.Generic;
using Core.Buses;
using Core.GridElements;
using Core.LevelGrids;
using Core.Passengers;
using Core.Signals;
using Frolics.Contexts;
using Frolics.Signals;
using Frolics.Tweens.Core;
using Frolics.Utilities;

namespace Core {
	// TODO Rename this
	public class TweenTimer : IInitializable {
		private IGridElementFactory gridElementFactory;
		private IBusFactory busFactory;

		private IPassengerController passengerController;
		private IBusController busController;
		private ISignalBus signalBus;

		private Dictionary<Bus, int> passengersBusWaits;
		private Queue<WaitingPassengerBoardSignal> waitingPassengersQueue;
		private Queue<BussFullSignal> busSequenceQueue;
		private HashSet<Bus> busesToLeave;

		void IInitializable.Initialize() {
			gridElementFactory = Context.Resolve<IGridElementFactory>();
			busFactory = Context.Resolve<IBusFactory>();

			passengerController = Context.Resolve<IPassengerController>();
			busController = Context.Resolve<IBusController>();
			signalBus = Context.Resolve<ISignalBus>();

			passengersBusWaits = new Dictionary<Bus, int>();
			waitingPassengersQueue = new Queue<WaitingPassengerBoardSignal>();
			busSequenceQueue = new Queue<BussFullSignal>();
			busesToLeave = new HashSet<Bus>();

			signalBus.SubscribeTo<PassengerBoardSignal>(OnPassengerBoard);
			signalBus.SubscribeTo<PassengerWaitSignal>(OnPassengerWait);
			signalBus.SubscribeTo<WaitingPassengerBoardSignal>(OnWaitingPassengerBoard);
			signalBus.SubscribeTo<BussFullSignal>(OnBusFull);
		}

		private void OnPassengerBoard(PassengerBoardSignal signal) {
			Bus bus = signal.Bus;
			Passenger passenger = signal.Passenger;
			LevelCell cell = signal.Cell;

			if (!passengersBusWaits.TryAdd(bus, 1))
				passengersBusWaits[bus] += 1;

			// TODO Try to avoid closure
			Tween tween = passengerController.PlayGridToBus(passenger, cell);
			tween.SetOnComplete(() => OnBoardTweenComplete(passenger, bus));
		}

		private void OnBoardTweenComplete(Passenger passenger, Bus bus) {
			gridElementFactory.Despawn(passenger);

			if (passengersBusWaits.ContainsKey(bus))
				passengersBusWaits[bus] -= 1;

			if (passengersBusWaits[bus] != 0 || !busesToLeave.Contains(bus))
				return;

			passengersBusWaits.Remove(bus);
			busesToLeave.Remove(bus);

			BussFullSignal bussFullSignal = busSequenceQueue.Dequeue();
			Tween tween = busController.PlayBusSequence(
				bussFullSignal.ArrivingBus,
				bussFullSignal.CurrentBus,
				bussFullSignal.LeavingBus
			);

			// TODO Try to avoid closure
			tween.SetOnComplete(() => OnBusTweenComplete(bussFullSignal.LeavingBus));
		}

		private void OnPassengerWait(PassengerWaitSignal signal) {
			passengerController.PlayGridToWaiting(signal.Passenger, signal.Cell, signal.WaitingCell);
		}

		private void OnWaitingPassengerBoard(WaitingPassengerBoardSignal signal) {
			waitingPassengersQueue.Enqueue(signal);
		}

		private void OnBusTweenComplete(Bus leavingBus) {
			busFactory.Despawn(leavingBus);
			CheckWaitingQueue();
		}

		private void CheckWaitingQueue() {
			List<WaitingPassengerBoardSignal> requeueList = new();
			while (waitingPassengersQueue.Count > 0) {
				WaitingPassengerBoardSignal signal = waitingPassengersQueue.Dequeue();
				Bus bus = signal.Bus;
				Passenger passenger = signal.Passenger;

				if (busSequenceQueue.TryPeek(out BussFullSignal bussFullSignal))
					if (bussFullSignal.LeavingBus != bus) {
						requeueList.Add(signal);
						continue;
					}

				if (!passengersBusWaits.TryAdd(bus, 1))
					passengersBusWaits[bus] += 1;

				Tween tween = passengerController.PlayWaitingToBus(passenger);
				tween.SetOnComplete(() => OnBoardTweenComplete(passenger, bus));
			}

			foreach (WaitingPassengerBoardSignal requeue in requeueList) {
				waitingPassengersQueue.Enqueue(requeue);
			}
		}

		private void OnBusFull(BussFullSignal signal) {
			busesToLeave.Add(signal.LeavingBus);
			busSequenceQueue.Enqueue(signal);
		}
	}
}
