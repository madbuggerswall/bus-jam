namespace Core.Mechanics {
	public interface ITimerManager {
		public void StartTimer();
		public void StopTimer();
		public float GetRemainingTime();
	}
}