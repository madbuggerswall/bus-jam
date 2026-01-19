using Core.LevelGrids;
using Core.Waiting.Grids;

namespace Core.Passengers {
	public interface IPassengerController {
		public void PlayWaitingToBus(Passenger passenger);
		public void PlayGridToBus(Passenger passenger, LevelCell cell);
		public void PlayGridToWaiting(Passenger passenger, LevelCell cell, WaitingCell waitingCell);
	}
}
