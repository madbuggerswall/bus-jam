using Core.LevelGrids;
using Core.Levels;
using Frolics.Utilities;

namespace Core.PathFinding {
	public class LevelGridManager : IInitializable, IGridBehaviourProvider, IGridProvider {
		private LevelGrid grid;
		private LevelGridBehaviour gridBehaviour;

		// Services
		private ILevelLoader levelLoader;

		void IInitializable.Initialize() {
			throw new System.NotImplementedException();
		}

		LevelGridBehaviour IGridBehaviourProvider.GetGridBehaviour() {
			throw new System.NotImplementedException();
		}

		LevelGrid IGridProvider.GetGrid() {
			throw new System.NotImplementedException();
		}
	}
}