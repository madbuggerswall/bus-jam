namespace Core.Waiting.Grids {
	public interface IWaitingGridBehaviourFactory {
		public WaitingGridBehaviour Create();
		void Despawn(WaitingGridBehaviour gridBehaviour);
	}
}