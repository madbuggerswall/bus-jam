using Core.LevelGrids;
using Core.Passengers;
using Core.Waiting.Grids;
using Frolics.Signals;

namespace Core.Mechanics.Signals {
	public struct PassengerWaitSignal : ISignal {
		public Passenger Passenger { get; }
		public LevelCell Cell { get; }
		public WaitingCell WaitingCell { get; }

		public PassengerWaitSignal(Passenger passenger, LevelCell cell, WaitingCell waitingCell) {
			Passenger = passenger;
			Cell = cell;
			WaitingCell = waitingCell;
		}
	}
}