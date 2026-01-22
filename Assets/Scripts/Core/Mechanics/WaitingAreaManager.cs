using System.Collections.Generic;
using Core.LevelGrids;
using Core.Mechanics.Signals;
using Core.Passengers;
using Core.PathFinding;
using Core.Waiting.Grids;
using Frolics.Contexts;
using Frolics.Signals;
using Frolics.Utilities;

namespace Core.Mechanics {
	public class WaitingAreaManager : IInitializable, IWaitingAreaManager {
		private readonly List<Passenger> passengers = new();

		// Services
		private ISignalBus signalBus;
		private IWaitingGridProvider waitingGridProvider;
		private ILevelAreaManager levelAreaManager;
		private IPathFinder pathFinder;


		void IInitializable.Initialize() {
			signalBus = Context.Resolve<ISignalBus>();
			waitingGridProvider = Context.Resolve<IWaitingGridProvider>();
			levelAreaManager = Context.Resolve<ILevelAreaManager>();
		}

		bool IWaitingAreaManager.HasEmptySlots() {
			WaitingCell[] cells = waitingGridProvider.GetGrid().GetCells();
			for (int i = 0; i < cells.Length; i++)
				if (!cells[i].HasPassenger())
					return true;

			return false;
		}

		bool IWaitingAreaManager.TryPlacePassenger(Passenger passenger) {
			WaitingGrid grid = waitingGridProvider.GetGrid();
			WaitingCell[] cells = grid.GetCells();
			LevelCell levelCell = levelAreaManager.GetCell(passenger);
			for (int i = 0; i < cells.Length; i++) {
				WaitingCell waitingCell = cells[i];
				if (waitingCell.HasPassenger())
					continue;

				levelAreaManager.RemovePassenger(passenger);
				grid.PlacePassengerAtCell(passenger, waitingCell);
				passengers.Add(passenger);
				signalBus.Fire(new PassengerWaitSignal(passenger, levelCell, waitingCell));

				return true;
			}

			return false;
		}

		public void RemovePassenger(Passenger passenger) {
			WaitingGrid grid = waitingGridProvider.GetGrid();
			grid.RemovePassenger(passenger);
			passengers.Remove(passenger);
		}

		public List<Passenger> GetPassengers() {
			return passengers;
		}
	}
}