using Frolics.Contexts;
using Frolics.Tweens.Core;
using Frolics.Tweens.Easing;
using Frolics.Tweens.Extensions;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Buses {
	public class BusController : MonoBehaviour, IInitializable, IBusController {
		[SerializeField] private Transform spawnPoint;
		[SerializeField] private Transform startPoint;
		[SerializeField] private Transform stopPoint;
		[SerializeField] private Transform exitPoint;

		private IBusManager busManager;

		void IInitializable.Initialize() {
			busManager = Context.Resolve<IBusManager>();
		}

		Tween IBusController.PlayBusSequence() {
			const float duration = 0.5f;

			Bus arrivingBus = busManager.GetArrivingBus();
			Bus currentBus = busManager.GetCurrentBus();
			Bus leavingBus = busManager.GetLeavingBus();

			Sequence sequence = Sequence.Create();
			sequence.Join(CreateArrivingBusTween(arrivingBus, duration));
			sequence.Join(CreateCurrentBusTween(currentBus, duration));
			sequence.Join(CreateLeavingBusTween(leavingBus, duration));
			sequence.Play();
			return sequence;
		}

		Tween CreateArrivingBusTween(Bus bus, float duration) {
			bus.transform.position = spawnPoint.position;
			Tween tween = bus.transform.TweenPosition(startPoint.position, duration);
			tween.SetEase(Ease.Type.InOutQuad);
			return tween;
		}

		Tween CreateCurrentBusTween(Bus bus, float duration) {
			bus.transform.position = startPoint.position;
			Tween tween = bus.transform.TweenPosition(stopPoint.position, duration);
			tween.SetEase(Ease.Type.InOutQuad);
			return tween;
		}

		Tween CreateLeavingBusTween(Bus bus, float duration) {
			bus.transform.position = stopPoint.position;
			Tween tween = bus.transform.TweenPosition(exitPoint.position, duration);
			tween.SetEase(Ease.Type.InOutQuad);
			return tween;
		}

		Tween IBusController.PlaySpawnToStartTween(Bus bus) {
			const float duration = 0.5f;

			bus.transform.position = spawnPoint.position;
			Tween tween = bus.transform.TweenPosition(startPoint.position, duration);
			tween.SetEase(Ease.Type.InOutQuad);
			return tween;
		}

		Tween IBusController.PlayStartToStopTween(Bus bus) {
			const float duration = 0.5f;
			bus.transform.position = startPoint.position;

			Tween tween = bus.transform.TweenPosition(stopPoint.position, duration);
			tween.SetEase(Ease.Type.InOutQuad);
			tween.Play();
			return tween;
		}

		Tween IBusController.PlayStopToExitTween(Bus bus) {
			const float duration = 0.5f;
			bus.transform.position = stopPoint.position;

			Tween tween = bus.transform.TweenPosition(exitPoint.position, duration);
			tween.SetEase(Ease.Type.InOutQuad);
			tween.Play();
			return tween;
		}
	}
}
