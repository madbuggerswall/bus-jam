using System.Collections.Generic;
using Core.LevelGrids;
using Frolics.Grids.SpatialHelpers;
using Frolics.Tweens.Core;
using Frolics.Tweens.Easing;
using Frolics.Tweens.Extensions;

namespace Core.Passengers {
	public class PassengerController {
		private readonly Passenger passenger;

		public PassengerController(Passenger passenger) {
			this.passenger = passenger;
		}

		public void PlayPathTween(LevelGrid grid, List<SquareCoord> coords) {
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