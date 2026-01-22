using UnityEngine;

namespace LevelEditor.EditorGrids {
	public interface IEditorLevelGridInitializer {
		public void CreateGrid();
		public void SetGridSize(Vector2Int gridSize);
	}
}