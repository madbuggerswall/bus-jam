using Core.Data;
using Core.LevelGrids;
using Core.Mechanics;
using Frolics.Contexts;
using Frolics.Signals;
using Frolics.Utilities;
using LevelEditor.BusGrids;
using LevelEditor.EditorGrids;
using LevelEditor.EditorInput;

namespace LevelEditor.SaveSystem {
	public class EditorLevelLoader : IInitializable, IEditorLevelLoader {
		// Services
		private ISignalBus signalBus;
		private ILevelDefinitionLoader levelDefinitionLoader;
		private IEditorLevelGridInitializer levelGridInitializer;
		private IEditorWaitingGridInitializer waitingGridInitializer;
		private IBusGridInitializer busGridInitializer;

		private ILevelGridProvider levelGridProvider;
		private IBusGridProvider busGridProvider;

		private IPassengerDTOSpawner passengerDTOSpawner;
		private IBusDTOSpawner busDTOSpawner;
		private IEditorCellBehaviourMapper cellBehaviourMapper;
		private ILevelTimeProvider levelTimeProvider;


		void IInitializable.Initialize() {
			signalBus = Context.Resolve<ISignalBus>();
			levelDefinitionLoader = Context.Resolve<ILevelDefinitionLoader>();
			levelGridInitializer = Context.Resolve<IEditorLevelGridInitializer>();
			waitingGridInitializer = Context.Resolve<IEditorWaitingGridInitializer>();
			busGridInitializer = Context.Resolve<IBusGridInitializer>();

			levelGridProvider = Context.Resolve<ILevelGridProvider>();
			busGridProvider = Context.Resolve<IBusGridProvider>();

			passengerDTOSpawner = Context.Resolve<IPassengerDTOSpawner>();
			busDTOSpawner = Context.Resolve<IBusDTOSpawner>();

			cellBehaviourMapper = Context.Resolve<IEditorCellBehaviourMapper>();
			levelTimeProvider = Context.Resolve<ILevelTimeProvider>();
		}

		void IEditorLevelLoader.LoadLevel() {
			LevelDefinition levelDefinition = levelDefinitionLoader.LoadLevelDefinition();
			if (levelDefinition == null)
				return;

			LevelDTO levelDTO = levelDefinition.GetLevelDTO();
			levelTimeProvider.SetLevelTime(levelDTO.GetLevelTime());

			levelGridInitializer.SetGridSize(levelDTO.GetLevelGridSize());
			levelGridInitializer.CreateGrid();

			waitingGridInitializer.SetGridSize(levelDTO.GetWaitingGridSize());
			waitingGridInitializer.CreateGrid();

			busGridInitializer.SetGridSize(levelDTO.GetBusGridSize());
			busGridInitializer.CreateGrid();

			// Set Cells
			CellDTO[] cellDTOs = levelDTO.GetCellDTOs();
			LevelGrid levelGrid = levelGridProvider.GetGrid();
			for (int i = 0; i < cellDTOs.Length; i++) {
				CellDTO cellDTO = cellDTOs[i];
				if (!levelGrid.TryGetCell(cellDTO.GetLocalCoord(), out LevelCell cell))
					continue;

				cell.SetReachable(!cellDTO.IsEmpty());
				if (cellBehaviourMapper.TryGetCellBehaviour(cell, out LevelCellBehaviour cellBehaviour))
					cellBehaviour.Initialize(cell);
			}

			// Spawn Passengers
			PassengerDTO[] passengerDTOs = levelDTO.GetPassengerDTOs();
			for (int i = 0; i < passengerDTOs.Length; i++)
				passengerDTOSpawner.SpawnPassenger(passengerDTOs[i]);

			// Spawn EditorBuses
			BusDTO[] busDTOs = levelDTO.GetBusDTOs();
			BusGrid busGrid = busGridProvider.GetGrid();
			BusCell[] busCells = busGrid.GetCells();
			for (int i = 0; i < busDTOs.Length; i++)
				busDTOSpawner.SpawnBus(busDTOs[i], busCells[i]);

			signalBus.Fire(new EditorLevelLoadedSignal(levelDTO));
		}
	}
}
