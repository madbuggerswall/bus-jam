using UnityEngine;

namespace LevelEditor.EditorGrids {
	public interface IEditorWaitingGridInitializer {
		public void CreateGrid();
		public void SetGridSize(Vector2Int gridSize);
	}
}