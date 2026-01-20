using Core.Data;
using Frolics.Tweens.Core;
using Frolics.Tweens.Easing;
using Frolics.Tweens.Extensions;
using UnityEngine;

namespace Core.Passengers.Types {
	public class CloakPassenger : Passenger {
		[SerializeField] private Transform cloakTransform;

		private bool canMove;

		public override void Initialize(ColorDefinition colorDefinition) {
			base.Initialize(colorDefinition);
			canMove = false;
			PlayCloakTween();
		}

		public override bool CanMove() => canMove;
		public override void OnNeighborMove() { }

		public override void OnAnyMove() {
			canMove = !canMove;
			PlayCloakTween();
		}

		private void PlayCloakTween() {
			const float duration = 0.5f;
			float targetScale = canMove ? .2f : 1f;

			Tween tween = cloakTransform.TweenLocalScaleY(targetScale, duration);
			tween.SetEase(Ease.Type.InOutQuad);
			tween.Play();
		}
	}
}
