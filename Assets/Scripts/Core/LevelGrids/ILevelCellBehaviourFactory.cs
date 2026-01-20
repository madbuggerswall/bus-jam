namespace Core.LevelGrids {
	public interface ILevelCellBehaviourFactory {
		public void CreateCellBehaviours(LevelGrid grid, LevelGridBehaviour gridBehaviour);
		public void Despawn(LevelCellBehaviour cellBehaviour);
	}
}
