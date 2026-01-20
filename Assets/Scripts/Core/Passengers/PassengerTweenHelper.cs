using System.Collections.Generic;
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

		public Tween PlayPathTween(List<Vector3> positions) {
			passenger.transform.position = positions[0];
			Sequence sequence = Sequence.Create();

			for (int i = 1; i < positions.Count; i++) {
				Tween positionTween = passenger.transform.TweenPosition(positions[i], 0.25f);
				positionTween.SetEase(i == 0 ? Ease.Type.InQuad : Ease.Type.Linear);
				sequence.Append(positionTween);
			}

			sequence.Play();
			return sequence;
		}
	}
}
