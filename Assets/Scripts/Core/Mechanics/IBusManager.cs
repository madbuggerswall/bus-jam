using Core.Passengers;

namespace Core.Mechanics {
	public interface IBusManager {
		public bool AreAllBusesFilled();
		public bool TryBoardPassenger(Passenger passenger);
	}
}
