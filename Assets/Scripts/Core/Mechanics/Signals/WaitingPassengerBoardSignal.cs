using Core.Buses;
using Core.Passengers;
using Frolics.Signals;

namespace Core.Mechanics.Signals {
	public struct WaitingPassengerBoardSignal : ISignal {
		public Passenger Passenger { get; }
		public Bus Bus { get; }

		public WaitingPassengerBoardSignal(Bus bus, Passenger passenger) {
			Passenger = passenger;
			Bus = bus;
		}
	}
}