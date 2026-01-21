using Frolics.Contexts;
using Frolics.Signals;
using Frolics.Utilities;

namespace Core.Levels {
	public class LevelStateManager : IInitializable, ILevelStateManager {
		private bool levelFailed;
		private bool levelSucceeded;

		// Services
		private ISignalBus signalBus;

		void IInitializable.Initialize() {
			signalBus = Context.Resolve<ISignalBus>();
		}

		bool ILevelStateManager.HasLevelEnded() {
			return levelFailed || levelSucceeded;
		}

		void ILevelStateManager.OnSuccess() {
			levelSucceeded = true;
			signalBus.Fire(new LevelSuccessSignal());
		}

		void ILevelStateManager.OnFail() {
			levelFailed = true;
			signalBus.Fire(new LevelFailSignal());
		}
	}

	public struct LevelSuccessSignal : ISignal { }

	public struct LevelFailSignal : ISignal { }
}
