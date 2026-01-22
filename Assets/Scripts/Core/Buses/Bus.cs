using Core.Data;
using Core.Passengers;
using Core.Passengers.Types;
using TMPro;
using UnityEngine;

namespace Core.Buses {
	public class Bus : MonoBehaviour, IColorable {
		[SerializeField] private MeshRenderer meshRenderer;
		[SerializeField] private TextMeshPro capacityText;
		[SerializeField] private TextMeshPro reservedCapacityText;

		private const int DefaultCapacity = 3;

		private ColorDefinition colorDefinition;

		private int capacity;
		private int reservedCapacity;
		private int passengerCount;
		private int reservedCount;

		public void Initialize(BusDTO busDTO) {
			SetColorDefinition(busDTO.GetColorDefinition());

			capacity = DefaultCapacity;
			reservedCapacity = busDTO.GetReservedCapacity();
			passengerCount = 0;
			reservedCount = 0;

			UpdateTexts();
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

			UpdateTexts();
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

		private void UpdateTexts() {
			capacityText.text = $"{capacity - reservedCapacity}";
			reservedCapacityText.text = $"R{reservedCapacity}";
		}
	}
}
