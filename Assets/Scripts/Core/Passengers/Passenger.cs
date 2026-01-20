using Core.Data;
using Core.GridElements;
using UnityEngine;

namespace Core.Passengers {
	public class Passenger : GridElement {
		[SerializeField] private MeshRenderer meshRenderer;
		[SerializeField] private Transform meshTransform;

		private PassengerColor color;
		private PassengerTweenHelper tweenHelper;
		private bool isReserved;

		public void Initialize(PassengerColor color, Material material) {
			meshRenderer.sharedMaterial = material;
			this.color = color;
			tweenHelper = new PassengerTweenHelper(this);
		}

		public PassengerColor GetColor() { return color; }
		public PassengerTweenHelper GetTweenHelper() { return tweenHelper; }
		public Transform GetMeshTransform() => meshTransform;
	}

	// IDEA Rename to PassengerTweenHelper
}
