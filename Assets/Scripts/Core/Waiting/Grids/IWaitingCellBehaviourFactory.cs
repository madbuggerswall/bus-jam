namespace Core.Waiting.Grids {
	public interface IWaitingCellBehaviourFactory {
		public void CreateCellBehaviours(WaitingGrid grid, WaitingGridBehaviour gridBehaviour);
		void Despawn(WaitingCellBehaviour cellBehaviour);
	}
}