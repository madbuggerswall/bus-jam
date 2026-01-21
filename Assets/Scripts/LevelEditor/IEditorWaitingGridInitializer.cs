using UnityEngine;

namespace LevelEditor {
	public interface IEditorWaitingGridInitializer {
		public void CreateGrid();
		public void SetGridSize(Vector2Int gridSize);
	}
}