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
			const GridPlane gridPlane = GridPlane.XZ;
			const float cellDiameter = 1f;

			// Create GridBehaviour
			LevelData levelData = levelDefinition.GetLevelData();
			Vector2Int gridSize = levelData.GetGridSize();
			gridBehaviour = gridBehaviourFactory.Create(gridSize, gridPlane);

			// Create Grid
			Vector3 pivotLocalPos = new(-gridSize.x / 2f + cellDiameter / 2, 0, -gridSize.y);
			CellData[] cellDTOs = levelData.GetCells();
			grid = new LevelGrid(gridBehaviour.transform, pivotLocalPos, gridSize, cellDiameter, GridPlane.XZ);

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
				Material material = colorManager.GetMaterial(passengerDTO.GetPassengerColor());
				passenger.Initialize(material);
			}
			
			// Create Buses
			BusData[] busDTOs = levelData.GetBuses();
			for (int i = 0; i < busDTOs.Length; i++) {
				BusData busDTO = busDTOs[i];
				
			}
		}
	}
}
