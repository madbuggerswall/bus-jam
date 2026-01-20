using Core.Buses;
using Core.LevelGrids;
using Core.Passengers;
using Core.PathFinding;
using Core.Waiting.Grids;
using Frolics.Contexts;
using Frolics.Signals;
using Frolics.Utilities;

namespace Core {
	public class RuleManager : IInitializable {
		// Services
		private ISignalBus signalBus;
		private IPathFinder pathFinder;
		private IBusManager busManager;
		private ILevelGridProvider gridProvider;
		private IWaitingGridProvider waitingGridProvider;
		private IWaitingAreaManager waitingAreaManager;
		private IPassengerController passengerController;

		void IInitializable.Initialize() {
			signalBus = Context.Resolve<ISignalBus>();
			pathFinder = Context.Resolve<IPathFinder>();
			busManager = Context.Resolve<IBusManager>();
			gridProvider = Context.Resolve<ILevelGridProvider>();
			waitingGridProvider = Context.Resolve<IWaitingGridProvider>();
			waitingAreaManager = Context.Resolve<IWaitingAreaManager>();
			passengerController = Context.Resolve<IPassengerController>();
		}

		public void OnPassengerSelected(Passenger passenger, LevelCell cell) {
			if (!pathFinder.IsTargetReachable(cell.GetCoord())) {
				passenger.GetTweenHelper().PlayUnreachableTween();
				return;
			}
			
			if (busManager.CanBoardPassenger(passenger)) {
				busManager.BoardPassenger(passenger);
				return;
			}

			if (waitingAreaManager.TryPlacePassenger(passenger)) {
				return;
			}
			
			// if (!waitingGrid.HasEmptyCells()) {
			// 	// TODO GameOver
			// }
		}
	}
}