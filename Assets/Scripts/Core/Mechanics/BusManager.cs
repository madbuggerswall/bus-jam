using System.Collections.Generic;
using Core.Buses;
using Core.Data;
using Core.LevelGrids;
using Core.Levels;
using Core.Mechanics.Signals;
using Core.Passengers;
using Frolics.Contexts;
using Frolics.Signals;
using Frolics.Utilities;

namespace Core.Mechanics {
	public class BusManager : IInitializable, IBusManager {
		private BusDTO[] busDTOs;
		private int currentIndex;
		private int busesFilled;

		private Bus arrivingBus;
		private Bus currentBus;
		private Bus leavingBus;

		// Services
		private ISignalBus signalBus;
		private IBusFactory busFactory;
		private ILevelLoader levelLoader;

		private ILevelAreaManager levelAreaManager;
		private IWaitingAreaManager waitingAreaManager;


		void IInitializable.Initialize() {
			signalBus = Context.Resolve<ISignalBus>();
			busFactory = Context.Resolve<IBusFactory>();
			levelLoader = Context.Resolve<ILevelLoader>();

			levelAreaManager = Context.Resolve<ILevelAreaManager>();
			waitingAreaManager = Context.Resolve<IWaitingAreaManager>();

			busDTOs = levelLoader.GetLevelData().GetBusDTOs();

			// Init
			BusDTO currentBusDTO = busDTOs[currentIndex++];
			currentBus = busFactory.CreateBus(currentBusDTO);

			BusDTO arrivingBusDTO = busDTOs[currentIndex++];
			arrivingBus = busFactory.CreateBus(arrivingBusDTO);
			
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
}
