using Core.LevelGrids;

namespace LevelEditor {
	public interface IEditorLevelCellSelector {
		public LevelCell GetSelectedCell();
	}
}