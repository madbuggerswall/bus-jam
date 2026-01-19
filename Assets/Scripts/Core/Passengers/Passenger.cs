using System.Collections.Generic;
using Core.Data;
using Core.GridElements;
using Core.LevelGrids;
using Frolics.Grids.SpatialHelpers;
using Frolics.Tweens.Core;
using Frolics.Tweens.Easing;
using Frolics.Tweens.Extensions;
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
	public class PassengerController {
		private readonly Passenger passenger;

		public PassengerController(Passenger passenger) {
			this.passenger = passenger;
		}

		public void PlayPathTween(LevelGrid grid, List<SquareCoord> coords) {
			Sequence sequence = Sequence.Create();

			for (int i = 0; i < coords.Count; i++) {
				Vector3 worldPosition = grid.ToWorldPosition(coords[i]);
				Tween positionTween = passenger.transform.TweenPosition(worldPosition, 0.5f);
				positionTween.SetEase(i == 0 ? Ease.Type.InQuad : Ease.Type.Linear);
				sequence.Append(positionTween);
			}

			sequence.Play();
		}
	}
}
