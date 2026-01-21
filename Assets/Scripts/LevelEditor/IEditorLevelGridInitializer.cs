using UnityEngine;

namespace LevelEditor {
	public interface IEditorLevelGridInitializer {
		public void CreateGrid();
		public void SetGridSize(Vector2Int gridSize);
	}
}