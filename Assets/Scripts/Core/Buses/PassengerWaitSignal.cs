using Core.LevelGrids;
using Core.Passengers;
using Core.Waiting.Grids;
using Frolics.Signals;

namespace Core.Buses {
	public struct PassengerWaitSignal : ISignal {
		public Passenger Passenger { get; }
		public LevelCell Cell { get; }
		public WaitingCell WaitingCell { get; }

		public PassengerWaitSignal(Passenger passenger, LevelCell cell, WaitingCell waitingCell) {
			this.Passenger = passenger;
			this.Cell = cell;
			this.WaitingCell = waitingCell;
		}
	}
}