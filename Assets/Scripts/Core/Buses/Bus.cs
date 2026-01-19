using Core.Data;
using UnityEngine;

namespace Core.Buses {
	public class Bus : MonoBehaviour {
		[SerializeField] private MeshRenderer meshRenderer;

		private const int DefaultCapacity = 3;

		private PassengerColor color;
		private int capacity;
		private int reservedCapacity;
		private int passengerCount;
		private int reservedCount = 0;

		public void Initialize(BusData busData, Material material) {
			color = busData.GetPassengerColor();
			capacity = DefaultCapacity;
			reservedCapacity = busData.GetReservedCount();
			passengerCount = 0;
			reservedCount = 0;

			meshRenderer.sharedMaterial = material;
		}

		public PassengerColor GetColor() => color;
		public int GetCapacity() => capacity;
		public int GetReservedCount() => reservedCount;
		public int GetReservedCapacity() => reservedCapacity;
		public int GetPassengerCount() => passengerCount;
	}
}
