using System;
using UnityEngine;

namespace Core.Data {
	[Serializable]
	public class LevelData {
		[SerializeField] private Vector2Int gridSize;
		[SerializeField] private Vector2Int waitingGridSize;
		[SerializeField] private CellData[] cells;
		[SerializeField] private PassengerData[] passengers;
		[SerializeField] private BusData[] buses;
		[SerializeField] private float levelTime;

		public Vector2Int GetGridSize() => gridSize;
		public Vector2Int GetWaitingGridSize() => waitingGridSize;
		public CellData[] GetCells() => cells;
		public PassengerData[] GetPassengers() => passengers;
		public BusData[] GetBuses() => buses;
		public float GetLevelTime() => levelTime;
	}
}
