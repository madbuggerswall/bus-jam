using Core.LevelGrids;
using Core.Waiting.Grids;
using Frolics.Tweens.Core;

namespace Core.Passengers {
	public interface IPassengerController {
		public Tween PlayWaitingToBus(Passenger passenger);
		public Tween PlayGridToBus(Passenger passenger, LevelCell cell);
		public Tween PlayGridToWaiting(Passenger passenger, LevelCell cell, WaitingCell waitingCell);
	}
}
