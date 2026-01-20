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
		private IBusManager busManager;
		private ISignalBus signalBus;
		private ILevelGridProvider levelGridProvider;

		private Dictionary<Bus, int> waitingPassengersByBus;
		private Queue<WaitingPassengerBoardSignal> waitingPassengersQueue;
		private HashSet<Bus> busesToLeave;

		void IInitializable.Initialize() {
			passengerController = Context.Resolve<IPassengerController>();
			busController = Context.Resolve<IBusController>();
			busManager = Context.Resolve<IBusManager>();
			signalBus = Context.Resolve<ISignalBus>();
			levelGridProvider = Context.Resolve<ILevelGridProvider>();

			waitingPassengersByBus = new Dictionary<Bus, int>();
			waitingPassengersQueue = new Queue<WaitingPassengerBoardSignal>();
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

			if (!waitingPassengersByBus.TryAdd(bus, 1))
				waitingPassengersByBus[bus] += 1;

			// TODO Try to avoid closure
			Tween tween = passengerController.PlayGridToBus(passenger, cell);
			tween.SetOnComplete(() => OnBoardTweenComplete(bus));
		}

		private void OnBoardTweenComplete(Bus bus) {
			if (waitingPassengersByBus.ContainsKey(bus))
				waitingPassengersByBus[bus] -= 1;

			if (waitingPassengersByBus[bus] == 0 && busesToLeave.Contains(bus)) {
				waitingPassengersByBus.Remove(bus);
				busesToLeave.Remove(bus);
				Tween tween = busController.PlayBusSequence();
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
			while (waitingPassengersQueue.Count > 0) {
				WaitingPassengerBoardSignal signal = waitingPassengersQueue.Dequeue();
				Bus bus = signal.Bus;
				Passenger passenger = signal.Passenger;

				if (!waitingPassengersByBus.TryAdd(bus, 1))
					waitingPassengersByBus[bus] += 1;

				Tween tween = passengerController.PlayWaitingToBus(passenger);
				tween.SetOnComplete(() => OnBoardTweenComplete(bus));
			}
		}

		private void OnBusFull(BussFullSignal signal) {
			busesToLeave.Add(signal.Bus);
		}
	}
}
