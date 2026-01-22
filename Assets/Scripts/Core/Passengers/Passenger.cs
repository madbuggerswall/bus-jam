using Core.Data;
using Core.GridElements;
using UnityEngine;

namespace Core.Passengers {
	public abstract class Passenger : GridElement, IColorable {
		[SerializeField] private PassengerDefinition passengerDefinition;
		[SerializeField] private Renderer meshRenderer;
		[SerializeField] private Transform meshTransform;

		private ColorDefinition colorDefinition;
		private PassengerTweenHelper tweenHelper;

		public virtual void Initialize(ColorDefinition colorDefinition) {
			SetColorDefinition(colorDefinition);
			tweenHelper = new PassengerTweenHelper(this);
		}

		public abstract bool CanMove();
		public abstract void OnNeighborMove();
		public abstract void OnAnyMove();

		// IColorable
		public ColorDefinition GetColorDefinition() {
			return colorDefinition;
		}

		// IColorable
		public void SetColorDefinition(ColorDefinition colorDefinition) {
			this.colorDefinition = colorDefinition;
			meshRenderer.sharedMaterial = colorDefinition.GetMaterial();
		}

		public PassengerDefinition GetPassengerDefinition() => passengerDefinition;
		public PassengerTweenHelper GetTweenHelper() { return tweenHelper; }
		public Transform GetMeshTransform() => meshTransform;
	}
}
