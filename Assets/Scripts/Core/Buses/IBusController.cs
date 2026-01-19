namespace Core.Buses {
	public interface IBusController {
		public void PlaySpawnToStartTween(Bus bus);
		public void PlayStartToStopTween(Bus bus);
		public void PlayStopToExitTween(Bus bus);
	}
}