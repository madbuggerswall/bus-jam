using Core.Data;
using Core.GridElements;
using UnityEngine;

namespace Core.Passengers {
	public interface IColorable {
		public ColorDefinition GetColorDefinition();
		public void SetColorDefinition(ColorDefinition colorDefinition);
	}

	public class Passenger : GridElement, IColorable {
		[SerializeField] private MeshRenderer meshRenderer;
		[SerializeField] private Transform meshTransform;

		private ColorDefinition colorDefinition;
		private PassengerTweenHelper tweenHelper;
		private bool isReserved;

		public void Initialize(ColorDefinition colorDefinition) {
			SetColorDefinition(colorDefinition);
			tweenHelper = new PassengerTweenHelper(this);
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

		public PassengerTweenHelper GetTweenHelper() { return tweenHelper; }
		public Transform GetMeshTransform() => meshTransform;

	}
}
