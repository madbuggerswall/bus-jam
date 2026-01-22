using Core.Buses;
using Frolics.Signals;

namespace Core.Signals {
	public struct BusesFullSignal : ISignal {
		public Bus ArrivingBus { get; }
		public Bus CurrentBus { get; }
		public Bus LeavingBus { get; }

		public BusesFullSignal(Bus arrivingBus, Bus currentBus, Bus leavingBus) {
			ArrivingBus = arrivingBus;
			CurrentBus = currentBus;
			LeavingBus = leavingBus;
		}
	}
}
