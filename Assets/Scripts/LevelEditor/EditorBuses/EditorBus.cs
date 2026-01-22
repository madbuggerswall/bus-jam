using Core.Data;
using Core.GridElements;
using Core.Passengers;
using TMPro;
using UnityEngine;

namespace LevelEditor.EditorBuses {
	public class EditorBus : GridElement, IColorable {
		[SerializeField] private MeshRenderer meshRenderer;
		[SerializeField] private TextMeshPro capacityText;
		[SerializeField] private TextMeshPro reservedCapacityText;

		private const int DefaultCapacity = 3;

		private int capacity;
		private int reservedCapacity;

		private ColorDefinition colorDefinition;

		public void Initialize(ColorDefinition colorDefinition) {
			SetColorDefinition(colorDefinition);
			capacity = DefaultCapacity;
			reservedCapacity = 0;
			UpdateTexts();
		}

		public void Initialize(BusDTO busDTO) {
			SetColorDefinition(busDTO.GetColorDefinition());
			capacity = busDTO.GetCapacity();
			reservedCapacity = busDTO.GetReservedCapacity();
			UpdateTexts();
		}

		public void SetReservedCapacity(int reservedCapacity) {
			this.reservedCapacity = reservedCapacity;
			UpdateTexts();
		}

		private void UpdateTexts() {
			capacityText.text = $"{capacity - reservedCapacity}";
			reservedCapacityText.text = $"R{reservedCapacity}";
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
		public int GetReservedCapacity() => reservedCapacity;
	}
}
