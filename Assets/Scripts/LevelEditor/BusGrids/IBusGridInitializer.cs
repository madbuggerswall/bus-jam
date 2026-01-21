using UnityEngine;

namespace LevelEditor.BusGrids {
	public interface IBusGridInitializer {
		public void CreateGrid();
		public void SetGridSize(Vector2Int gridSize);
	}
}