using Frolics.Tweens.Core;

namespace Core.Buses {
	public interface IBusController {
		public Tween PlayBusSequence(Bus arrivingBus, Bus currentBus, Bus leavingBus);
	}
}
