using Core.Data;
using Core.LevelGrids;
using Core.Passengers;
using Frolics.Contexts;
using Frolics.Grids;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Levels {
	public class LevelLoader : MonoBehaviour, IInitializable {
		[SerializeField] private LevelDefinition defaultLevelDefinition;

		private LevelGridBehaviour gridBehaviour;
		private LevelGrid grid;

		// Services
		private ILevelGridBehaviourFactory gridBehaviourFactory;
		private IPassengerFactory passengerFactory;

		void IInitializable.Initialize() {
			gridBehaviourFactory = Context.Resolve<ILevelGridBehaviourFactory>();
			passengerFactory = Context.Resolve<IPassengerFactory>();
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
			for (int i = 0; i < cellDTOs.Length; i++) {
				CellData cellDTO = cellDTOs[i];
				if (grid.TryGetCell(cellDTO.GetLocalCoord(), out LevelCell cell))
					cell.SetReachable(cellDTO.GetCellType() is not CellType.Empty);
			}

			// Create Passengers
			PassengerData[] passengerDTOs = levelData.GetPassengers();
			for (int i = 0; i < passengerDTOs.Length; i++) {
				PassengerData passengerDTO = passengerDTOs[i];
				Passenger passenger = passengerFactory.Create(passengerDTO.GetPassengerType());
			}
		}
	}
}
