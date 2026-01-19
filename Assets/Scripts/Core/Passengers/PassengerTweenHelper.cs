using System.Collections.Generic;
using Core.LevelGrids;
using Core.Waiting.Grids;
using Frolics.Grids.SpatialHelpers;
using Frolics.Tweens.Core;
using Frolics.Tweens.Easing;
using Frolics.Tweens.Extensions;
using UnityEngine;

namespace Core.Passengers {
	public class PassengerTweenHelper {
		private readonly Passenger passenger;
		private readonly Vector3 meshScale;

		public PassengerTweenHelper(Passenger passenger) {
			this.passenger = passenger;
			meshScale = passenger.GetMeshTransform().localScale;
		}

		public void PlayUnreachableTween() {
			const float targetScale = 0.64f;
			const float duration = 0.1f;

			Transform transform = passenger.GetMeshTransform();
			transform.localScale = meshScale;

			Tween tween = transform.TweenLocalScale(targetScale * Vector3.one, duration);
			tween.SetCycles(Tween.CycleType.Reflect, 2, false, true);
			tween.SetEase(Ease.Type.OutQuad);
			tween.Play();
		}

		public void PlayPathTween(LevelGrid grid, List<SquareCoord> coords) { }

		public void PlayBusTween() { }

		public void PlayPathTween(List<Vector3> positions) {
			passenger.transform.position = positions[0];
			Sequence sequence = Sequence.Create();

			for (int i = 1; i < positions.Count; i++) {
				Tween positionTween = passenger.transform.TweenPosition(positions[i], 0.25f);
				positionTween.SetEase(i == 0 ? Ease.Type.InQuad : Ease.Type.Linear);
				sequence.Append(positionTween);
			}

			sequence.Play();
		}

		public void PlayWaitingAreTween(WaitingGrid grid, WaitingCell cell, List<SquareCoord> coords) {
			passenger.transform.position = grid.ToWorldPosition(coords[0]);
			Sequence sequence = Sequence.Create();

			for (int i = 1; i < coords.Count; i++) {
				Tween positionTween = passenger.transform.TweenPosition(grid.ToWorldPosition(coords[i]), 0.25f);
				positionTween.SetEase(i == 0 ? Ease.Type.InQuad : Ease.Type.Linear);

				sequence.Append(positionTween);
			}

			sequence.Play();
		}
	}
}
