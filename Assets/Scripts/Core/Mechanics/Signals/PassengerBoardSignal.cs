using Core.Buses;
using Core.LevelGrids;
using Core.Passengers;
using Frolics.Signals;

namespace Core.Mechanics.Signals {
	public struct PassengerBoardSignal : ISignal {
		public Passenger Passenger { get; }
		public Bus Bus { get; }
		public LevelCell Cell { get; }

		public PassengerBoardSignal(Bus bus, Passenger passenger, LevelCell cell) {
			Passenger = passenger;
			Bus = bus;
			Cell = cell;
		}
	}
}