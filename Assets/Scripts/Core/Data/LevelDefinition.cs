using System;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Core.Data {
	[CreateAssetMenu(menuName = MenuName, fileName = FileName)]
	public class LevelDefinition : ScriptableObject {
		private const string MenuName = "Definition/Levels/" + FileName;
		private const string FileName = nameof(LevelDefinition);

		[SerializeField] private LevelData levelData;
		public LevelData GetLevelData() => levelData;
	}

	[Serializable]
	public class LevelData {
		[SerializeField] private Vector2Int gridSize;
		[SerializeField] private CellData[] cells;
		[SerializeField] private PassengerData[] passengers;
		[SerializeField] private BusData[] buses;
		[SerializeField] private float levelTime;

		public Vector2Int GetGridSize() => gridSize;
		public CellData[] GetCells() => cells;
		public PassengerData[] GetPassengers() => passengers;
		public BusData[] GetBuses() => buses;
		public float GetLevelTime() => levelTime;
	}

	[Serializable]
	public struct PassengerData {
		[SerializeField] private SquareCoord localCoord;
		[SerializeField] private PassengerColor passengerColor;
		[SerializeField] private PassengerType passengerType;

		public SquareCoord GetLocalCoord() => localCoord;
		public PassengerColor GetPassengerColor() => passengerColor;
		public PassengerType GetPassengerType() => passengerType;
	}

	[Serializable]
	public struct BusData {
		[SerializeField] private PassengerColor passengerColor;

		public PassengerColor GetPassengerColor() => passengerColor;
	}

	[Serializable]
	public struct CellData {
		[SerializeField] private CellType cellType;
		[SerializeField] private SquareCoord localCoord;

		public CellType GetCellType() => cellType;
		public SquareCoord GetLocalCoord() => localCoord;
	}

	public enum CellType { Default, Empty }

	public enum PassengerType { Default, Reserved, Secret, Cloak, Rope }

	// IDEA ColorManager
	// IDEA ColorDefinition and ColorDefinitionManager
	public enum PassengerColor { Blue, Brown, Cyan, Green, Orange, Purple, Pink, Red, Yellow, White }
}
