using Frolics.Tweens.Core;
using Frolics.Tweens.Easing;
using Frolics.Tweens.Extensions;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Buses {
	public interface IBusController {
		public void PlaySpawnToStartTween(Bus bus);
		public void PlayStartToStopTween(Bus bus);
		public void PlayStopToExitTween(Bus bus);
	}

	public class BusController : MonoBehaviour, IInitializable, IBusController {
		[SerializeField] private Transform spawnPoint;
		[SerializeField] private Transform startPoint;
		[SerializeField] private Transform stopPoint;
		[SerializeField] private Transform exitPoint;

		void IInitializable.Initialize() { }

		void IBusController.PlaySpawnToStartTween(Bus bus) {
			const float duration = 0.5f;
			bus.transform.position = spawnPoint.position;

			Tween tween = bus.transform.TweenPosition(startPoint.position, duration);
			tween.SetEase(Ease.Type.InOutQuad);
			tween.Play();
		}

		void IBusController.PlayStartToStopTween(Bus bus) {
			const float duration = 0.5f;
			bus.transform.position = startPoint.position;

			Tween tween = bus.transform.TweenPosition(stopPoint.position, duration);
			tween.SetEase(Ease.Type.InOutQuad);
			tween.Play();
		}

		void IBusController.PlayStopToExitTween(Bus bus) {
			const float duration = 0.5f;
			bus.transform.position = stopPoint.position;

			Tween tween = bus.transform.TweenPosition(exitPoint.position, duration);
			tween.SetEase(Ease.Type.InOutQuad);
			tween.Play();
		}
	}
}
