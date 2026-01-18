using Frolics.Utilities;

namespace Core.Levels {
	public interface ILevelStateManager {
		public void OnSuccess();
		public void OnFail();
	}

	public class LevelStateManager : IInitializable, ILevelStateManager {
		void IInitializable.Initialize() {
			throw new System.NotImplementedException();
		}

		void ILevelStateManager.OnSuccess() {
			throw new System.NotImplementedException();
		}

		void ILevelStateManager.OnFail() {
			throw new System.NotImplementedException();
		}
	}
}
