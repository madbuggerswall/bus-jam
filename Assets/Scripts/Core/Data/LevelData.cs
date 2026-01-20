using System;
using UnityEngine;

namespace Core.Data {
	[Serializable]
	public class LevelData {
		[SerializeField] private Vector2Int gridSize;
		[SerializeField] private Vector2Int waitingGridSize;
		[SerializeField] private CellDTO[] cells;
		[SerializeField] private PassengerDTO[] passengers;
		[SerializeField] private BusDTO[] buses;
		[SerializeField] private float levelTime;

		public Vector2Int GetGridSize() => gridSize;
		public Vector2Int GetWaitingGridSize() => waitingGridSize;
		public CellDTO[] GetCells() => cells;
		public PassengerDTO[] GetPassengers() => passengers;
		public BusDTO[] GetBuses() => buses;
		public float GetLevelTime() => levelTime;
	}
}
