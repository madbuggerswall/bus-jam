using Core.Data;
using Core.Levels;
using Frolics.Contexts;
using Frolics.Grids;
using Frolics.Utilities;
using UnityEngine;

namespace Core.LevelGrids {
	public class LevelGridInitializer : IInitializable, ILevelGridBehaviourProvider, ILevelGridProvider {
		private LevelGrid grid;
		private LevelGridBehaviour gridBehaviour;

		// Services
		private ILevelLoader levelLoader;
		private ILevelGridBehaviourFactory gridBehaviourFactory;
		private ILevelCellBehaviourFactory cellBehaviourFactory;

		void IInitializable.Initialize() {
			levelLoader = Context.Resolve<ILevelLoader>();
			gridBehaviourFactory = Context.Resolve<ILevelGridBehaviourFactory>();
			cellBehaviourFactory = Context.Resolve<ILevelCellBehaviourFactory>();

			// Create Grid & GridBehaviour
			gridBehaviour = gridBehaviourFactory.Create();
			grid = CreateLevelGrid();
			cellBehaviourFactory.CreateCellBehaviours(grid, gridBehaviour);
		}

		private LevelGrid CreateLevelGrid() {
			const GridPlane gridPlane = GridPlane.XZ;
			const float cellDiameter = 1f;

			LevelData levelData = levelLoader.GetLevelData();
			Vector2Int gridSize = levelData.GetGridSize();
			CellData[] cellDTOs = levelData.GetCells();

			Vector3 pivotLocalPos = new(-gridSize.x / 2f + cellDiameter / 2, 0, -gridSize.y);
			LevelGrid grid = new(gridBehaviour.transform, pivotLocalPos, gridSize, cellDiameter, gridPlane);

			// Set empty cells
			for (int i = 0; i < cellDTOs.Length; i++)
				if (grid.TryGetCell(cellDTOs[i].GetLocalCoord(), out LevelCell cell))
					cell.SetReachable(cellDTOs[i].GetCellType() is not CellType.Empty);

			return grid;
		}

		LevelGridBehaviour ILevelGridBehaviourProvider.GetGridBehaviour() => gridBehaviour;
		LevelGrid ILevelGridProvider.GetGrid() => grid;
	}
}
