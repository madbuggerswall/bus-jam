using System.Collections.Generic;
using Core.Data;
using Core.GridElements;
using Frolics.Grids.SpatialHelpers;
using Frolics.Tweens.Core;
using Frolics.Tweens.Easing;
using Frolics.Tweens.Extensions;
using UnityEngine;

namespace Core.Passengers {
	public class Passenger : GridElement {
		[SerializeField] private MeshRenderer meshRenderer;

		private PassengerColor color;

		public void Initialize(Material material) {
			meshRenderer.sharedMaterial = material;
		}

		public PassengerColor GetColor() { return color; }
	}

	// This should not be a per passenger class or operation
	public class PassengerController {
		private LevelGrid grid;
		private Passenger passenger;

		// TODO PlayPathTween(cells, destination)
		private void PlayPathTween(LevelCell[] cells) {
			Sequence sequence = Sequence.Create();
			for (int i = 0; i < cells.Length; i++) {
				LevelCell cell = cells[i];
				Vector3 position = grid.GetWorldPosition(cell);

				Tween positionTween = passenger.transform.TweenPosition(position, 0.5f);
				positionTween.SetEase(i == 0 ? Ease.Type.InQuad : Ease.Type.Linear);

				sequence.Append(positionTween);
			}

			sequence.Play();
		}

		private void PlayPathTween(LevelGrid grid, List<SquareCoord> coords) {
			Sequence sequence = Sequence.Create();

			for (int i = 0; i < coords.Count; i++) {
				SquareCoord worldCoord = grid.ToWorldCoord(coords[i]);
				Vector3 worldPosition = grid.ToWorldPosition(worldCoord);

				Tween positionTween = passenger.transform.TweenPosition(worldPosition, 0.5f);
				positionTween.SetEase(i == 0 ? Ease.Type.InQuad : Ease.Type.Linear);

				sequence.Append(positionTween);
			}

			sequence.Play();
		}
	}
}
