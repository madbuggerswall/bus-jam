using Core.Levels;
using Frolics.Contexts;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Mechanics {
	public interface ITimerManager {
		public void StartTimer();
		public float GetRemainingTime();
	}

	public class TimerManager : MonoBehaviour, IInitializable, ITimerManager {
		private float levelTime;
		private float passedTime;
		private bool timerStarted;

		// Services
		private ILevelStateManager levelStateManager;
		private ILevelLoader levelLoader;

		void IInitializable.Initialize() {
			levelStateManager = Context.Resolve<ILevelStateManager>();
			levelLoader = Context.Resolve<ILevelLoader>();

			levelTime = levelLoader.GetLevelData().GetLevelTime();
			passedTime = 0;
			timerStarted = false;
		}

		void ITimerManager.StartTimer() => timerStarted = true;
		float ITimerManager.GetRemainingTime() => Mathf.Max(levelTime - passedTime, 0f);

		private void Update() => Tick();

		private void Tick() {
			if (!timerStarted)
				return;

			passedTime += Time.deltaTime;
			if (levelTime - passedTime <= 0)
				levelStateManager.OnFail();
		}
	}
}
