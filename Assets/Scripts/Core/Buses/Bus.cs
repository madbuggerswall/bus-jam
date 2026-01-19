using Core.Data;
using Core.Passengers;
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

		public bool CanPassengerRide(Passenger passenger) {
			PassengerColor passengerColor = passenger.GetColor();
			if (color != passengerColor)
				return false;

			if (passenger is ReservedPassenger)
				return reservedCapacity - reservedCount > 0;

			return capacity - reservedCapacity - passengerCount > 0;
		}

		public void HavePassenger(Passenger passenger) {
			if (passenger is ReservedPassenger)
				reservedCount++;
			else
				passengerCount++;
		}

		public bool IsFull() {
			return (passengerCount + reservedCount) == capacity;
		}

		public PassengerColor GetColor() => color;
		public int GetCapacity() => capacity;
		public int GetReservedCount() => reservedCount;
		public int GetReservedCapacity() => reservedCapacity;
		public int GetPassengerCount() => passengerCount;
	}

	public class ReservedBus : Bus { }
}
