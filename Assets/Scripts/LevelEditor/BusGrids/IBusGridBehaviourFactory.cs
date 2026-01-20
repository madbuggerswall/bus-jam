namespace LevelEditor.BusGrids {
	public interface IBusGridBehaviourFactory {
		public BusGridBehaviour Create();
		public void Despawn(BusGridBehaviour gridBehaviour);
	}
}