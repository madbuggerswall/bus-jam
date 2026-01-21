using UnityEngine;

namespace LevelEditor {
	public interface IBusGridInitializer {
		public void CreateGrid();
		public void SetGridSize(Vector2Int gridSize);
	}
}