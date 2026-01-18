using Core.GridElements;
using Frolics.Tweens.Core;
using Frolics.Tweens.Easing;
using Frolics.Tweens.Extensions;
using UnityEngine;

namespace Core.Passengers {
	public class Passenger : GridElement { }

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
	}
}
