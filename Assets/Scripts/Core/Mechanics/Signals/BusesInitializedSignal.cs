using Core.Buses;
using Frolics.Signals;

namespace Core.Mechanics.Signals {
	public struct BusesInitializedSignal : ISignal {
		public Bus ArrivingBus { get; }
		public Bus CurrentBus { get; }
		public Bus LeavingBus { get; }

		public BusesInitializedSignal(Bus arrivingBus, Bus currentBus, Bus leavingBus) {
			ArrivingBus = arrivingBus;
			CurrentBus = currentBus;
			LeavingBus = leavingBus;
		}
	}
}