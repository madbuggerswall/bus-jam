using Core.LevelGrids;

namespace LevelEditor.Tools {
	public interface IEditorLevelCellSelector {
		public LevelCell GetSelectedCell();
	}
}