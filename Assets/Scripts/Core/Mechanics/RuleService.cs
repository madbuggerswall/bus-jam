using Core.LevelGrids;
using Core.Levels;
using Core.Passengers;
using Core.PathFinding;
using Frolics.Contexts;
using Frolics.Grids.SpatialHelpers;
using Frolics.Utilities;

namespace Core.Mechanics {
	public class RuleService : IInitializable, IRuleService {
		// Services
		private IPathFinder pathFinder;
		private IBusManager busManager;
		private ILevelGridProvider gridProvider;
		private IWaitingAreaManager waitingAreaManager;
		private ILevelStateManager levelStateManager;
		private ITimerManager timerManager;

		void IInitializable.Initialize() {
			pathFinder = Context.Resolve<IPathFinder>();
			busManager = Context.Resolve<IBusManager>();
			gridProvider = Context.Resolve<ILevelGridProvider>();
			waitingAreaManager = Context.Resolve<IWaitingAreaManager>();
			levelStateManager = Context.Resolve<ILevelStateManager>();
			timerManager = Context.Resolve<ITimerManager>();
		}

		void IRuleService.OnPassengerSelected(Passenger passenger, LevelCell cell) {
			if (levelStateManager.HasLevelEnded())
				return;

			if (!pathFinder.IsTargetReachable(cell.GetCoord())) {
				passenger.GetTweenHelper().PlayUnreachableTween();
				return;
			}

			if (busManager.TryBoardPassenger(passenger)) {
				timerManager.StartTimer();
				NotifyNeighbors(cell);
				NotifyAll();
				CheckLevelState();
				return;
			}

			if (waitingAreaManager.TryPlacePassenger(passenger)) {
				timerManager.StartTimer();
				NotifyNeighbors(cell);
				NotifyAll();
				CheckLevelState();
				return;
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

		private void CheckLevelState() {
			if (!waitingAreaManager.HasEmptySlots()) {
				levelStateManager.OnFail();
				timerManager.StopTimer();
			}

			if (busManager.AreAllBusesFilled()) {
				levelStateManager.OnSuccess();
				timerManager.StopTimer();
			}
		}
	}
}
