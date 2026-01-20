using System.Collections.Generic;
using Core.Data;
using Core.LevelGrids;
using Core.Levels;
using Core.Passengers;
using Core.PathFinding;
using Core.Waiting.Grids;
using Frolics.Contexts;
using Frolics.Signals;
using Frolics.Utilities;

namespace Core.Buses {
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

		private ILevelAreaManager levelAreaManager;
		private IWaitingAreaManager waitingAreaManager;
		private IPathFinder pathFinder;


		void IInitializable.Initialize() {
			signalBus = Context.Resolve<ISignalBus>();
			busFactory = Context.Resolve<IBusFactory>();
			levelLoader = Context.Resolve<ILevelLoader>();
			busController = Context.Resolve<IBusController>();

			levelAreaManager = Context.Resolve<ILevelAreaManager>();
			waitingAreaManager = Context.Resolve<IWaitingAreaManager>();
			pathFinder = Context.Resolve<IPathFinder>();

			busDTOs = levelLoader.GetLevelData().GetBuses();

			// Init
			BusData currentBusData = busDTOs[currentIndex++];
			currentBus = busFactory.CreateBus(currentBusData);

			BusData arrivingBusData = busDTOs[currentIndex++];
			arrivingBus = busFactory.CreateBus(arrivingBusData);

			// TODO TweenTimer needs to play this
			busController.PlayBusSequence(arrivingBus, currentBus, leavingBus);
		}

		public void BoardPassenger(Passenger passenger) {
			LevelCell cell = levelAreaManager.GetCell(passenger);

			currentBus.HavePassenger(passenger);
			levelAreaManager.RemovePassenger(passenger);

			signalBus.Fire(new PassengerBoardSignal(currentBus, passenger, cell));

			if (currentBus.IsFull())
				OnBusFull();
		}

		public void BoardWaitingPassenger(Passenger passenger) {
			currentBus.HavePassenger(passenger);
			waitingAreaManager.RemovePassenger(passenger);
			signalBus.Fire(new WaitingPassengerBoardSignal(currentBus, passenger));

			if (currentBus.IsFull())
				OnBusFull();
		}

		public bool CanBoardPassenger(Passenger passenger) {
			return currentBus.CanBoarPassenger(passenger);
		}

		private void OnBusFull() {
			leavingBus = currentBus;
			currentBus = arrivingBus;

			if (currentIndex >= busDTOs.Length) {
				arrivingBus = null;
			} else {
				BusData nextBusData = busDTOs[currentIndex++];
				arrivingBus = busFactory.CreateBus(nextBusData);
			}


			signalBus.Fire(new BussFullSignal(arrivingBus, currentBus, leavingBus));
			BoardWaitingPassengers();
		}

		private void BoardWaitingPassengers() {
			List<Passenger> passengers = waitingAreaManager.GetPassengers();
			for (int i = passengers.Count - 1; i >= 0; i--) {
				Passenger passenger = passengers[i];
				if (CanBoardPassenger(passenger))
					BoardWaitingPassenger(passenger);
			}
		}

		Bus IBusManager.GetCurrentBus() => currentBus;
		Bus IBusManager.GetArrivingBus() => arrivingBus;
		Bus IBusManager.GetLeavingBus() => leavingBus;
	}
}
