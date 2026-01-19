using Core.Data;
using Core.GridElements;
using UnityEngine;

namespace Core.Passengers {
	public class Passenger : GridElement {
		[SerializeField] private MeshRenderer meshRenderer;

		private PassengerColor color;
		private PassengerController controller;

		public void Initialize(PassengerColor color, Material material) {
			meshRenderer.sharedMaterial = material;
			this.color = color;
			controller = new PassengerController(this);
		}

		public PassengerColor GetColor() { return color; }
		public PassengerController GetController() { return controller; }
	}

	// IDEA Rename to PassengerTweenHelper
}
