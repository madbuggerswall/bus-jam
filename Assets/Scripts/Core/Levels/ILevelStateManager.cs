namespace Core.Levels {
	public interface ILevelStateManager {
		public bool HasLevelEnded();
		public void OnSuccess();
		public void OnFail();
	}
}
