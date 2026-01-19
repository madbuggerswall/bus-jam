using Core.LevelGrids;

namespace Core.Levels {
	public interface IGridProvider {
		public LevelGrid GetGrid();
	}
}