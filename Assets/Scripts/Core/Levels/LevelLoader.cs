using Core.Data;
using Core.LevelGrids;
using Core.Passengers;
using Frolics.Contexts;
using Frolics.Grids;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Levels {
	public interface ILevelLoader { }

	public class LevelLoader : MonoBehaviour, IInitializable, ILevelLoader {
		[SerializeField] private LevelDefinition defaultLevelDefinition;

		private LevelGridBehaviour gridBehaviour;
		private LevelGrid grid;

		// Services
		private ILevelGridBehaviourFactory gridBehaviourFactory;
		private ILevelCellBehaviourFactory cellBehaviourFactory;
		private IPassengerSpawner passengerSpawner;

		void IInitializable.Initialize() {
			gridBehaviourFactory = Context.Resolve<ILevelGridBehaviourFactory>();
			cellBehaviourFactory = Context.Resolve<ILevelCellBehaviourFactory>();
			passengerSpawner = Context.Resolve<IPassengerSpawner>();

			LoadLevel(defaultLevelDefinition);
		}

		public void LoadLevel(LevelDefinition levelDefinition) {
			const GridPlane gridPlane = GridPlane.XZ;

			// Create GridBehaviour
			LevelData levelData = levelDefinition.GetLevelData();
			Vector2Int gridSize = levelData.GetGridSize();
			gridBehaviour = gridBehaviourFactory.Create(gridSize, gridPlane);

			// Create Grid
			Vector3 pivotLocalPos = new((float) gridSize.x / 2 - 1, 0, (float) gridSize.y / 2 - 1);
			CellData[] cellDTOs = levelData.GetCells();
			grid = new LevelGrid(gridBehaviour.transform, pivotLocalPos, gridSize, 1f, GridPlane.XZ);

			// Set empty cells
			for (int i = 0; i < cellDTOs.Length; i++)
				if (grid.TryGetCell(cellDTOs[i].GetLocalCoord(), out LevelCell cell))
					cell.SetReachable(cellDTOs[i].GetCellType() is not CellType.Empty);

			// Create CellBehaviours
			cellBehaviourFactory.CreateCellBehaviours(grid, gridBehaviour.GetCellRoot());

			// Create Passengers
			PassengerData[] passengerDTOs = levelData.GetPassengers();
			for (int i = 0; i < passengerDTOs.Length; i++) {
				PassengerData passengerDTO = passengerDTOs[i];
				if (!grid.TryGetCell(passengerDTO.GetLocalCoord(), out LevelCell cell))
					continue;

				Passenger passenger = passengerSpawner.Spawn(passengerDTO.GetPassengerType(), grid, cell);
			}
		}
	}
}
