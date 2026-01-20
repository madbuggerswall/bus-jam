using Frolics.Signals;

namespace Core.Buses {
	public struct BussFullSignal : ISignal {
		public Bus Bus { get; }

		public BussFullSignal(Bus bus) {
			this.Bus = bus;
		}
	}
}