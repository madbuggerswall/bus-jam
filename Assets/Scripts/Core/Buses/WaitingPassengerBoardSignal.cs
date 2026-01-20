using Core.Passengers;
using Frolics.Signals;

namespace Core.Buses {
	public struct WaitingPassengerBoardSignal : ISignal {
		public Passenger Passenger { get; }
		public Bus Bus { get; }

		public WaitingPassengerBoardSignal(Bus bus, Passenger passenger) {
			this.Passenger = passenger;
			this.Bus = bus;
		}
	}
}