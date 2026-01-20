namespace LevelEditor.BusGrids {
	public interface IBusCellBehaviourFactory {
		public void CreateCellBehaviours(BusGrid grid, BusGridBehaviour gridBehaviour);
		public void Despawn(BusCellBehaviour cellBehaviour);
	}
}