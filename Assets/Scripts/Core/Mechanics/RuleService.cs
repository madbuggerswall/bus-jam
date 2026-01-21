using Core.LevelGrids;
using Core.Levels;
using Core.Passengers;
using Core.PathFinding;
using Core.Waiting.Grids;
using Frolics.Contexts;
using Frolics.Grids.SpatialHelpers;
using Frolics.Signals;
using Frolics.Utilities;

namespace Core.Mechanics {
	public class RuleService : IInitializable, IRuleService {
		// Services
		private ISignalBus signalBus;
		private IPathFinder pathFinder;
		private IBusManager busManager;
		private ILevelGridProvider gridProvider;
		private IWaitingGridProvider waitingGridProvider;
		private IWaitingAreaManager waitingAreaManager;
		private IPassengerController passengerController;
		private ILevelStateManager levelStateManager;

		void IInitializable.Initialize() {
			signalBus = Context.Resolve<ISignalBus>();
			pathFinder = Context.Resolve<IPathFinder>();
			busManager = Context.Resolve<IBusManager>();
			gridProvider = Context.Resolve<ILevelGridProvider>();
			waitingGridProvider = Context.Resolve<IWaitingGridProvider>();
			waitingAreaManager = Context.Resolve<IWaitingAreaManager>();
			passengerController = Context.Resolve<IPassengerController>();
		}

		void IRuleService.OnPassengerSelected(Passenger passenger, LevelCell cell) {
			if (!pathFinder.IsTargetReachable(cell.GetCoord())) {
				passenger.GetTweenHelper().PlayUnreachableTween();
				return;
			}

			if (busManager.TryBoardPassenger(passenger)) {
				NotifyNeighbors(cell);
				NotifyAll();
				return;
			}

			if (waitingAreaManager.TryPlacePassenger(passenger)) {
				NotifyNeighbors(cell);
				NotifyAll();
				return;
			}

			if (!waitingAreaManager.HasEmptySlots()) {
				levelStateManager.OnFail();
			}
		}

		private void NotifyNeighbors(LevelCell cell) {
			LevelGrid grid = gridProvider.GetGrid();
			for (int i = 0; i < SquareCoord.DirectionVectors.Length; i++) {
				SquareCoord neighborCoord = cell.GetCoord() + SquareCoord.DirectionVectors[i];
				if (!grid.TryGetCell(neighborCoord, out LevelCell neighborCell))
					continue;

				if (!neighborCell.HasElement() || neighborCell.GetGridElement() is not Passenger neighborPassenger)
					continue;

				neighborPassenger.OnNeighborMove();
			}
		}

		private void NotifyAll() {
			LevelGrid grid = gridProvider.GetGrid();
			LevelCell[] cells = grid.GetCells();
			for (int i = 0; i < cells.Length; i++) {
				LevelCell cell = cells[i];
				if (!cell.HasElement() || cell.GetGridElement() is not Passenger passenger)
					continue;

				passenger.OnAnyMove();
			}
		}
	}
}
