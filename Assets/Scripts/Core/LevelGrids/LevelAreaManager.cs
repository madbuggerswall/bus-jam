using Core.Passengers;
using Core.PathFinding;
using Frolics.Contexts;
using Frolics.Utilities;

namespace Core.LevelGrids {
	public interface ILevelAreaManager {
		public void RemovePassenger(Passenger passenger);
		public LevelCell GetCell(Passenger passenger);
	}

	public class LevelAreaManager : IInitializable, ILevelAreaManager {
		// Services 
		private ILevelGridProvider levelGridProvider;
		private IPathFinder pathFinder;

		void IInitializable.Initialize() {
			levelGridProvider = Context.Resolve<ILevelGridProvider>();
			pathFinder = Context.Resolve<IPathFinder>();
		}

		void ILevelAreaManager.RemovePassenger(Passenger passenger) {
			levelGridProvider.GetGrid().RemoveElement(passenger);
			pathFinder.OnGridModified();
		}

		LevelCell ILevelAreaManager.GetCell(Passenger passenger) {
			return levelGridProvider.GetGrid().GetCell(passenger);
		}
	}
}
