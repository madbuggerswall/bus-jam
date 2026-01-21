using Core.Passengers;

namespace Core.Mechanics {
	public interface IBusManager {
		public bool TryBoardPassenger(Passenger passenger);
	}
}