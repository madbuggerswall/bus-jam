namespace Core.LevelGrids {
	public interface ILevelGridBehaviourFactory {
		public LevelGridBehaviour Create();
		public void Despawn(LevelGridBehaviour gridBehaviour);
	}
}
