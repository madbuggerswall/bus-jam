using Frolics.Tweens.Core;

namespace Core.Buses {
	public interface IBusController {
		public Tween PlayBusSequence();
		public Tween PlaySpawnToStartTween(Bus bus);
		public Tween PlayStartToStopTween(Bus bus);
		public Tween PlayStopToExitTween(Bus bus);
	}
}
