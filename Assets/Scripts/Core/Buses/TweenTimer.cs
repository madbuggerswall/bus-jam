using System.Collections.Generic;
using Core.LevelGrids;
using Core.Passengers;
using Core.Waiting.Grids;
using Frolics.Contexts;
using Frolics.Signals;
using Frolics.Tweens.Core;
using Frolics.Utilities;

namespace Core.Buses {
	public class TweenTimer : IInitializable {
		private IPassengerController passengerController;
		private IBusController busController;
		private ISignalBus signalBus;

		private Dictionary<Bus, int> passengersBusWaits;
		private Queue<WaitingPassengerBoardSignal> waitingPassengersQueue;
		private Queue<BussFullSignal> busSequenceQueue;
		private HashSet<Bus> busesToLeave;

		void IInitializable.Initialize() {
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
			tween.SetOnComplete(() => OnBoardTweenComplete(bus));
		}

		private void OnBoardTweenComplete(Bus bus) {
			if (passengersBusWaits.ContainsKey(bus))
				passengersBusWaits[bus] -= 1;

			if (passengersBusWaits[bus] == 0 && busesToLeave.Contains(bus)) {
				passengersBusWaits.Remove(bus);
				busesToLeave.Remove(bus);
				BussFullSignal bussFullSignal = busSequenceQueue.Dequeue();
				Bus arrivingBus = bussFullSignal.ArrivingBus;
				Bus currentBus = bussFullSignal.CurrentBus;
				Bus leavingBus = bussFullSignal.LeavingBus;

				Tween tween = busController.PlayBusSequence(arrivingBus, currentBus, leavingBus);
				tween.SetOnComplete(CheckWaitingQueue);
			}
		}

		private void OnPassengerWait(PassengerWaitSignal signal) {
			Passenger passenger = signal.Passenger;
			LevelCell cell = signal.Cell;
			WaitingCell waitingCell = signal.WaitingCell;
			passengerController.PlayGridToWaiting(passenger, cell, waitingCell);
		}

		private void OnWaitingPassengerBoard(WaitingPassengerBoardSignal signal) {
			waitingPassengersQueue.Enqueue(signal);
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
				tween.SetOnComplete(() => OnBoardTweenComplete(bus));
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
