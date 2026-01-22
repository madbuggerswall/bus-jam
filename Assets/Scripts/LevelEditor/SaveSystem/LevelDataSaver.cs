using System.Collections.Generic;
using Core.Data;
using Core.LevelGrids;
using Core.Mechanics;
using Core.Passengers;
using Core.Waiting.Grids;
using Frolics.Contexts;
using Frolics.Grids.SpatialHelpers;
using Frolics.Utilities;
using LevelEditor.BusGrids;
using UnityEngine;

namespace LevelEditor.SaveSystem {
	public class LevelDataSaver : IInitializable, ILevelDataSaver {
		// Services
		private ILevelGridProvider levelGridProvider;
		private IWaitingGridProvider waitingGridProvider;
		private IBusGridProvider busGridProvider;
		private ILevelTimeProvider levelTimeProvider;

		void IInitializable.Initialize() {
			levelGridProvider = Context.Resolve<ILevelGridProvider>();
			waitingGridProvider = Context.Resolve<IWaitingGridProvider>();
			busGridProvider = Context.Resolve<IBusGridProvider>();
			levelTimeProvider = Context.Resolve<ILevelTimeProvider>();
		}

		LevelDTO ILevelDataSaver.SaveLevelData() {
			Vector2Int levelGridSize = levelGridProvider.GetGrid().GetGridSize();
			Vector2Int waitingGridSize = waitingGridProvider.GetGrid().GetGridSize();
			Vector2Int busGridSize = busGridProvider.GetGrid().GetGridSize();
			float levelTime = levelTimeProvider.GetLevelTime();
			CellDTO[] cellDTOs = SaveCellData();
			PassengerDTO[] passengerDTOs = SavePassengerData();
			BusDTO[] busDTOs = SaveBusData();

			return new LevelDTO(
				levelGridSize,
				waitingGridSize,
				busGridSize,
				levelTime,
				cellDTOs,
				passengerDTOs,
				busDTOs
			);
		}

		private CellDTO[] SaveCellData() {
			List<CellDTO> cellDTOs = new();
			LevelGrid grid = levelGridProvider.GetGrid();
			LevelCell[] cells = grid.GetCells();

			for (int i = 0; i < cells.Length; i++) {
				LevelCell cell = cells[i];

				SquareCoord localCoord = cell.GetCoord();
				bool isEmpty = !cell.IsReachable();
				CellDTO cellDTO = new(localCoord, isEmpty);
				cellDTOs.Add(cellDTO);
			}

			return cellDTOs.ToArray();
		}

		private PassengerDTO[] SavePassengerData() {
			List<PassengerDTO> passengerDTOs = new();
			LevelGrid grid = levelGridProvider.GetGrid();
			LevelCell[] cells = grid.GetCells();

			for (int i = 0; i < cells.Length; i++) {
				LevelCell cell = cells[i];
				if (!cell.HasElement() || cell.GetGridElement() is not Passenger passenger)
					continue;

				SquareCoord localCoord = cell.GetCoord();
				ColorDefinition colorDefinition = passenger.GetColorDefinition();
				PassengerDefinition passengerDefinition = passenger.GetPassengerDefinition();
				PassengerDTO passengerDTO = new(localCoord, colorDefinition, passengerDefinition);
				passengerDTOs.Add(passengerDTO);
			}

			return passengerDTOs.ToArray();
		}

		private BusDTO[] SaveBusData() {
			List<BusDTO> busDTOs = new();
			BusGrid busGrid = busGridProvider.GetGrid();
			BusCell[] cells = busGrid.GetCells();

			for (int i = 0; i < cells.Length; i++) {
				BusCell cell = cells[i];
				if (!cell.HasBus())
					continue;

				EditorBus bus = cell.GetBus();
				ColorDefinition colorDefinition = bus.GetColorDefinition();
				int capacity = bus.GetCapacity();
				int reservedCapacity = bus.GetReservedCapacity();

				BusDTO busDTO = new(colorDefinition, capacity, reservedCapacity);
				busDTOs.Add(busDTO);
			}

			return busDTOs.ToArray();
		}
	}
}
