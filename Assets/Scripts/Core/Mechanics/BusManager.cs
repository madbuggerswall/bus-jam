using System.Collections.Generic;
using Core.Buses;
using Core.Data;
using Core.LevelGrids;
using Core.Levels;
using Core.Passengers;
using Core.Signals;
using Frolics.Contexts;
using Frolics.Signals;
using Frolics.Utilities;

namespace Core.Mechanics {
	public class BusManager : IInitializable, IBusManager {
		private BusDTO[] busDTOs;
		private int currentIndex = 0;
		private int busesFilled = 0;

		private Bus arrivingBus;
		private Bus currentBus;
		private Bus leavingBus;

		// Services
		private ISignalBus signalBus;
		// private IBusController busController;
		private IBusFactory busFactory;
		private ILevelLoader levelLoader;

		private ILevelAreaManager levelAreaManager;
		private IWaitingAreaManager waitingAreaManager;


		void IInitializable.Initialize() {
			signalBus = Context.Resolve<ISignalBus>();
			busFactory = Context.Resolve<IBusFactory>();
			levelLoader = Context.Resolve<ILevelLoader>();
			// busController = Context.Resolve<IBusController>();

			levelAreaManager = Context.Resolve<ILevelAreaManager>();
			waitingAreaManager = Context.Resolve<IWaitingAreaManager>();

			busDTOs = levelLoader.GetLevelData().GetBusDTOs();

			// Init
			BusDTO currentBusDTO = busDTOs[currentIndex++];
			currentBus = busFactory.CreateBus(currentBusDTO);

			BusDTO arrivingBusDTO = busDTOs[currentIndex++];
			arrivingBus = busFactory.CreateBus(arrivingBusDTO);
			
			// TODO TweenTimer needs to play this
			// TODO OnBusesInitialized
			signalBus.Fire(new BusesInitializedSignal(arrivingBus, currentBus, leavingBus));
		}

		bool IBusManager.TryBoardPassenger(Passenger passenger) {
			if (!currentBus.CanBoardPassenger(passenger))
				return false;

			BoardPassenger(passenger);
			return true;
		}

		bool IBusManager.AreAllBusesFilled() {
			return busesFilled == busDTOs.Length;
		}

		private void BoardPassenger(Passenger passenger) {
			LevelCell cell = levelAreaManager.GetCell(passenger);

			currentBus.HavePassenger(passenger);
			levelAreaManager.RemovePassenger(passenger);

			signalBus.Fire(new PassengerBoardSignal(currentBus, passenger, cell));

			if (currentBus.IsFull())
				OnBusFull();
		}

		private void OnBusFull() {
			busesFilled++;
			leavingBus = currentBus;
			currentBus = arrivingBus;

			if (currentIndex >= busDTOs.Length) {
				arrivingBus = null;
			} else {
				BusDTO nextBusDTO = busDTOs[currentIndex++];
				arrivingBus = busFactory.CreateBus(nextBusDTO);
			}

			signalBus.Fire(new BusesFullSignal(arrivingBus, currentBus, leavingBus));
			BoardWaitingPassengers();
		}

		private void BoardWaitingPassengers() {
			List<Passenger> passengers = waitingAreaManager.GetPassengers();
			for (int i = passengers.Count - 1; i >= 0; i--) {
				Passenger passenger = passengers[i];
				if (currentBus.CanBoardPassenger(passenger))
					BoardWaitingPassenger(passenger);
			}
		}

		private void BoardWaitingPassenger(Passenger passenger) {
			currentBus.HavePassenger(passenger);
			waitingAreaManager.RemovePassenger(passenger);
			signalBus.Fire(new WaitingPassengerBoardSignal(currentBus, passenger));

			if (currentBus.IsFull())
				OnBusFull();
		}
	}

	public struct BusesInitializedSignal : ISignal {
		public Bus ArrivingBus { get; }
		public Bus CurrentBus { get; }
		public Bus LeavingBus { get; }

		public BusesInitializedSignal(Bus arrivingBus, Bus currentBus, Bus leavingBus) {
			ArrivingBus = arrivingBus;
			CurrentBus = currentBus;
			LeavingBus = leavingBus;
		}
	}
}
