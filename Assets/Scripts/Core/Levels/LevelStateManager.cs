using Frolics.Utilities;
using UnityEngine;

namespace Core.Levels {
	public class LevelStateManager : IInitializable, ILevelStateManager {
		private bool levelFailed;
		private bool levelSucceeded;
		void IInitializable.Initialize() { }

		bool ILevelStateManager.HasLevelEnded() {
			return levelFailed || levelSucceeded;
		}

		void ILevelStateManager.OnSuccess() {
			Debug.Log("Success");
			levelSucceeded = true;
		}

		void ILevelStateManager.OnFail() {
			Debug.Log("Fail");
			levelFailed = true;
		}
	}
}
