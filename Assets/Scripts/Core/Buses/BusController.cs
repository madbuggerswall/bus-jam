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


		void IInitializable.Initialize() { }

		Tween IBusController.PlayBusSequence(Bus arrivingBus, Bus currentBus, Bus leavingBus) {
			const float duration = 0.5f;

			Sequence sequence = Sequence.Create();
			if (arrivingBus != null)
				sequence.Join(CreateArrivingBusTween(arrivingBus, duration));

			if (currentBus != null)
				sequence.Join(CreateCurrentBusTween(currentBus, duration));

			if (leavingBus != null)
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
	}
}
