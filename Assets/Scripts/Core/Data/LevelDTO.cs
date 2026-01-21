using System;
using UnityEngine;

namespace Core.Data {
	[Serializable]
	public class LevelDTO {
		[SerializeField] private Vector2Int levelGridSize;
		[SerializeField] private Vector2Int waitingGridSize;
		[SerializeField] private Vector2Int busGridSize;
		[SerializeField] private float levelTime;
		[SerializeField] private CellDTO[] cellDTOs;
		[SerializeField] private PassengerDTO[] passengerDTOs;
		[SerializeField] private BusDTO[] busDTOs;

		public LevelDTO(
			Vector2Int levelGridSize,
			Vector2Int waitingGridSize,
			Vector2Int busGridSize,
			float levelTime,
			CellDTO[] cellDTOs,
			PassengerDTO[] passengerDTOs,
			BusDTO[] busDTOs
		) {
			this.levelGridSize = levelGridSize;
			this.waitingGridSize = waitingGridSize;
			this.busGridSize = busGridSize;
			this.levelTime = levelTime;
			this.cellDTOs = cellDTOs;
			this.passengerDTOs = passengerDTOs;
			this.busDTOs = busDTOs;
		}

		public Vector2Int GetLevelGridSize() => levelGridSize;
		public Vector2Int GetWaitingGridSize() => waitingGridSize;
		public Vector2Int GetBusGridSize() => busGridSize;
		public float GetLevelTime() => levelTime;
		public CellDTO[] GetCellDTOs() => cellDTOs;
		public PassengerDTO[] GetPassengerDTOs() => passengerDTOs;
		public BusDTO[] GetBusDTOs() => busDTOs;
	}
}
