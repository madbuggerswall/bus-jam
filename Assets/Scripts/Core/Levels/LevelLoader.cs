using Core.Data;
using Core.LevelGrids;
using Core.Passengers;
using Frolics.Contexts;
using Frolics.Grids;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Levels {
	public interface IGridBehaviourProvider {
		public LevelGridBehaviour GetGridBehaviour();
	}

	public interface IGridProvider {
		public LevelGrid GetGrid();
	}

	public interface ILevelLoader {
		public LevelData GetLevelData();
	}

	public class LevelLoader : MonoBehaviour, IInitializable, ILevelLoader, IGridBehaviourProvider, IGridProvider {
		[SerializeField] private LevelDefinition defaultLevelDefinition;

		private LevelGridBehaviour gridBehaviour;
		private LevelGrid grid;

		// Services
		private IPassengerColorManager colorManager;
		private ILevelGridBehaviourFactory gridBehaviourFactory;
		private ILevelCellBehaviourFactory cellBehaviourFactory;
		private IPassengerSpawner passengerSpawner;

		void IInitializable.Initialize() {
			colorManager = Context.Resolve<IPassengerColorManager>();
			gridBehaviourFactory = Context.Resolve<ILevelGridBehaviourFactory>();
			cellBehaviourFactory = Context.Resolve<ILevelCellBehaviourFactory>();
			passengerSpawner = Context.Resolve<IPassengerSpawner>();

			LoadLevel(defaultLevelDefinition);
		}

		public void LoadLevel(LevelDefinition levelDefinition) {
			// Create GridBehaviour
			LevelData levelData = levelDefinition.GetLevelData();
			gridBehaviour = gridBehaviourFactory.Create();
			grid = CreateLevelGrid(levelData);
			cellBehaviourFactory.CreateCellBehaviours(grid, gridBehaviour);

			passengerSpawner.SpawnPassengers(levelData, grid);

			// Create Buses
			BusData[] busDTOs = levelData.GetBuses();
			for (int i = 0; i < busDTOs.Length; i++) {
				BusData busDTO = busDTOs[i];
			}
		}

		private LevelGrid CreateLevelGrid(LevelData levelData) {
			const GridPlane gridPlane = GridPlane.XZ;
			const float cellDiameter = 1f;

			Vector2Int gridSize = levelData.GetGridSize();
			CellData[] cellDTOs = levelData.GetCells();

			Vector3 pivotLocalPos = new(-gridSize.x / 2f + cellDiameter / 2, 0, -gridSize.y);
			LevelGrid grid = new LevelGrid(gridBehaviour.transform, pivotLocalPos, gridSize, cellDiameter, gridPlane);

			// Set empty cells
			for (int i = 0; i < cellDTOs.Length; i++)
				if (grid.TryGetCell(cellDTOs[i].GetLocalCoord(), out LevelCell cell))
					cell.SetReachable(cellDTOs[i].GetCellType() is not CellType.Empty);

			return grid;
		}

		LevelData ILevelLoader.GetLevelData() => defaultLevelDefinition.GetLevelData();
		LevelGridBehaviour IGridBehaviourProvider.GetGridBehaviour() => gridBehaviour;
		LevelGrid IGridProvider.GetGrid() => grid;
	}
}
