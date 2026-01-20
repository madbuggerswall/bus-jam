using System.Collections.Generic;
using Core.Data;
using Core.LevelGrids;
using Core.Levels;
using Core.Passengers;
using Core.Waiting.Grids;
using Frolics.Contexts;
using Frolics.Signals;
using Frolics.Tweens.Core;
using Frolics.Utilities;

namespace Core.Buses {
	public interface IBusManager {
		public void OnBusFill();
		public Bus GetCurrentBus();
		public Bus GetArrivingBus();
		public Bus GetLeavingBus();
	}

	public class TweenTimer : IInitializable {
		private IPassengerController passengerController;
		private IBusController busController;
		private IBusManager busManager;
		private ISignalBus signalBus;

		private Dictionary<Bus, int> waitingPassengersByBus;
		private Queue<WaitingPassengerBoardSignal> waitingPassengersQueue;
		private HashSet<Bus> busesToLeave;

		void IInitializable.Initialize() {
			passengerController = Context.Resolve<IPassengerController>();
			busController = Context.Resolve<IBusController>();
			busManager = Context.Resolve<IBusManager>();
			signalBus = Context.Resolve<ISignalBus>();

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
			while(waitingPassengersQueue.Count > 0) {
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

	public struct PassengerBoardSignal : ISignal {
		public Passenger Passenger { get; }
		public Bus Bus { get; }
		public LevelCell Cell { get; }

		public PassengerBoardSignal(Bus bus, Passenger passenger, LevelCell cell) {
			this.Passenger = passenger;
			this.Bus = bus;
			this.Cell = cell;
		}
	}

	public struct PassengerWaitSignal : ISignal {
		public Passenger Passenger { get; }
		public LevelCell Cell { get; }
		public WaitingCell WaitingCell { get; }

		public PassengerWaitSignal(Passenger passenger, LevelCell cell, WaitingCell waitingCell) {
			this.Passenger = passenger;
			this.Cell = cell;
			this.WaitingCell = waitingCell;
		}
	}

	public struct WaitingPassengerBoardSignal : ISignal {
		public Passenger Passenger { get; }
		public Bus Bus { get; }

		public WaitingPassengerBoardSignal(Bus bus, Passenger passenger) {
			this.Passenger = passenger;
			this.Bus = bus;
		}
	}

	public struct BussFullSignal : ISignal {
		public Bus Bus { get; }

		public BussFullSignal(Bus bus) {
			this.Bus = bus;
		}
	}

	public class BusManager : IInitializable, IBusManager {
		private BusData[] busDTOs;
		private int currentIndex = 0;

		private Bus arrivingBus;
		private Bus currentBus;
		private Bus leavingBus;

		// Services
		private ISignalBus signalBus;
		private IBusController busController;
		private IBusFactory busFactory;
		private ILevelLoader levelLoader;


		void IInitializable.Initialize() {
			signalBus = Context.Resolve<ISignalBus>();
			busFactory = Context.Resolve<IBusFactory>();
			levelLoader = Context.Resolve<ILevelLoader>();
			busController = Context.Resolve<IBusController>();

			busDTOs = levelLoader.GetLevelData().GetBuses();

			// Init
			BusData currentBusData = busDTOs[currentIndex++];
			BusData arrivingBusData = busDTOs[currentIndex++];

			currentBus = busFactory.CreateBus(currentBusData);
			busController.PlayStartToStopTween(currentBus);

			arrivingBus = busFactory.CreateBus(arrivingBusData);
			busController.PlaySpawnToStartTween(arrivingBus);
		}

		void IBusManager.OnBusFill() {
			signalBus.Fire(new BussFullSignal(currentBus));

			leavingBus = currentBus;
			currentBus = arrivingBus;
			BusData nextBusData = busDTOs[currentIndex++];
			arrivingBus = busFactory.CreateBus(nextBusData);

			if (currentIndex < busDTOs.Length) { }

			// busController.PlayStopToExitTween(currentBus);
			// busController.PlayStartToStopTween(currentBus);
			// busController.PlaySpawnToStartTween(nextBus);
			// TODO Signal
		}

		Bus IBusManager.GetCurrentBus() => currentBus;
		Bus IBusManager.GetArrivingBus() => arrivingBus;
		Bus IBusManager.GetLeavingBus() => leavingBus;
	}
}
