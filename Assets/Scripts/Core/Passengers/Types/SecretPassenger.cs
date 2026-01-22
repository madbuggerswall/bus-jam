using Core.Data;
using Frolics.Tweens.Core;
using Frolics.Tweens.Easing;
using Frolics.Tweens.Extensions;
using Frolics.Utilities.Extensions;
using UnityEngine;

namespace Core.Passengers.Types {
	public class SecretPassenger : Passenger {
		[SerializeField] private Transform secretTransform;

		private bool isSecret;

		public override void Initialize(ColorDefinition colorDefinition) {
			base.Initialize(colorDefinition);
			isSecret = true;
			secretTransform.localScale = secretTransform.localScale.WithY(1f);
		}

		public override bool CanMove() => true;

		public override void OnNeighborMove() {
			if (!isSecret)
				return;

			isSecret = false;
			PlaySecretTween();
		}

		public override void OnAnyMove() { }

		private void PlaySecretTween() {
			const float duration = 0.5f;
			float targetScale = isSecret ? 1f : 0.1f;

			Tween tween = secretTransform.TweenLocalScaleY(targetScale, duration);
			tween.SetEase(Ease.Type.InOutQuad);
			tween.Play();
		}
	}
}
