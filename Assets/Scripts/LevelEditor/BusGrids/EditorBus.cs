using Core.Data;
using Core.GridElements;
using Core.Passengers;
using UnityEngine;

namespace LevelEditor.BusGrids {
	public class EditorBus : GridElement, IColorable {
		[SerializeField] private MeshRenderer meshRenderer;
		[SerializeField] private int reservedCount;

		private ColorDefinition colorDefinition;

		public void Initialize(ColorDefinition colorDefinition) {
			SetColorDefinition(colorDefinition);
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
		
		public int GetReservedCount() => reservedCount;
	}
}
