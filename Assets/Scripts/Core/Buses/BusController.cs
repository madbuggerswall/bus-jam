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
				sequence.Join(CreateBusTween(arrivingBus, spawnPoint, startPoint, duration));

			if (currentBus != null)
				sequence.Join(CreateBusTween(currentBus, startPoint, stopPoint, duration));

			if (leavingBus != null)
				sequence.Join(CreateBusTween(leavingBus, stopPoint, exitPoint, duration));

			sequence.Play();
			return sequence;
		}

		private static Tween CreateBusTween(Bus bus, Transform start, Transform target, float duration) {
			bus.transform.position = start.position;
			Tween tween = bus.transform.TweenPosition(target.position, duration);
			tween.SetEase(Ease.Type.InOutQuad);
			return tween;
		}
	}
}
