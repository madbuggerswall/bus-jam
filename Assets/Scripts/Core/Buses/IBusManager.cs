using Core.Passengers;

namespace Core.Buses {
	public interface IBusManager {
		public void BoardPassenger(Passenger passenger);
		public bool CanBoardPassenger(Passenger passenger);
		public Bus GetCurrentBus();
		public Bus GetArrivingBus();
		public Bus GetLeavingBus();
	}
}