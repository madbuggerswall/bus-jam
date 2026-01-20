using Core.Data;
using Core.Levels;
using Frolics.Contexts;
using Frolics.Grids;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Waiting.Grids {
	public class WaitingGridInitializer : IInitializable, IWaitingGridBehaviourProvider, IWaitingGridProvider {
		private WaitingGrid grid;
		private WaitingGridBehaviour gridBehaviour;

		// Services
		private ILevelLoader levelLoader;
		private IWaitingGridBehaviourFactory gridBehaviourFactory;
		private IWaitingCellBehaviourFactory cellBehaviourFactory;

		void IInitializable.Initialize() {
			levelLoader = Context.Resolve<ILevelLoader>();
			gridBehaviourFactory = Context.Resolve<IWaitingGridBehaviourFactory>();
			cellBehaviourFactory = Context.Resolve<IWaitingCellBehaviourFactory>();

			// Create Grid & GridBehaviour
			gridBehaviour = gridBehaviourFactory.Create();
			grid = CreateGrid();
			cellBehaviourFactory.CreateCellBehaviours(grid, gridBehaviour);
		}

		private WaitingGrid CreateGrid() {
			const GridPlane gridPlane = GridPlane.XZ;
			const float cellDiameter = 1f;

			LevelDTO levelDTO = levelLoader.GetLevelData();
			Vector2Int gridSize = levelDTO.GetWaitingGridSize();

			Vector3 pivotLocalPos = new(-gridSize.x / 2f + cellDiameter / 2, 0, -gridSize.y / 2f + cellDiameter / 2);
			WaitingGrid grid = new(gridBehaviour.transform, pivotLocalPos, gridSize, cellDiameter, gridPlane);

			return grid;
		}

		WaitingGridBehaviour IWaitingGridBehaviourProvider.GetWaitingGridBehaviour() => gridBehaviour;
		WaitingGrid IWaitingGridProvider.GetGrid() => grid;
	}
}
