using Core.Data;
using Core.Passengers;
using UnityEngine;

namespace Core.Buses {
	public class Bus : MonoBehaviour, IColorable {
		[SerializeField] private MeshRenderer meshRenderer;

		private const int DefaultCapacity = 3;

		private ColorDefinition colorDefinition;

		private int capacity;
		private int reservedCapacity;
		private int passengerCount;
		private int reservedCount = 0;

		public void Initialize(BusData busData, ColorDefinition colorDefinition) {
			SetColorDefinition(colorDefinition);

			capacity = DefaultCapacity;
			reservedCapacity = busData.GetReservedCount();
			passengerCount = 0;
			reservedCount = 0;
		}

		public bool CanBoardPassenger(Passenger passenger) {
			if (colorDefinition != passenger.GetColorDefinition())
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

		// IColorable
		public ColorDefinition GetColorDefinition() {
			return colorDefinition;
		}

		// IColorable
		public void SetColorDefinition(ColorDefinition colorDefinition) {
			this.colorDefinition = colorDefinition;
			meshRenderer.sharedMaterial = colorDefinition.GetMaterial();
		}

		public int GetCapacity() => capacity;
		public int GetReservedCount() => reservedCount;
		public int GetReservedCapacity() => reservedCapacity;
		public int GetPassengerCount() => passengerCount;
	}
}
