using Core.LevelGrids;
using Frolics.Grids;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Segments.Grids {
	[System.Serializable]
	public class LevelGridPreview : MonoBehaviour {
		[Header("Grid Settings")]
		[SerializeField] private Vector2Int gridSize;
		[SerializeField] private Vector3 pivotLocalPos;
		[SerializeField] private float cellDiameter;

		[Header("Visual Settings")]
		[SerializeField] private Color32 lightCellColor = new(0xDD, 0xDD, 0x00, 0x29);
		[SerializeField] private Color32 darkCellColor = new(0x8F, 0x8F, 0x00, 0x29);

		[Header("Preview Options")]
		[SerializeField] private bool showInSceneView = true;
		[SerializeField] private bool showCell = true;
		[SerializeField] private bool showCellWireframe = true;
		[SerializeField] private bool showGridWireframe = true;
		[SerializeField] private Color32 cellWireframeColor = Color.gray;
		[SerializeField] private Color32 gridWireframeColor = new(0xFF, 0xEE, 0x00, 0x7A);
		[SerializeField, Range(0.01f, 1f)] private float cellHeight = 0.01f;

		private LevelGrid grid;

		private void OnDrawGizmos() {
			if (!showInSceneView)
				return;

			DrawGrid();
		}

		private void DrawGrid() {
			grid = new LevelGrid(transform, pivotLocalPos, gridSize, cellDiameter, GridPlane.XZ);
			DrawGrid(grid);
			CulledGizmos.DrawSphere(transform.TransformPoint(pivotLocalPos), 0.25f, Color.yellow);
		}

		private void DrawGrid(LevelGrid grid) {
			LevelCell[] cells = grid.GetCells();
			Vector2Int gridSize = grid.GetGridSize();

			for (int i = 0; i < cells.Length; i++) {
				bool isLightCell = (i / gridSize.x + i) % 2 == 0;
				Vector3 position = grid.GetWorldPosition(cells[i]);

				DrawCell(position, grid.GetCellDiameter(), isLightCell);
			}

			if (showGridWireframe)
				DrawGridWireframe(grid.GetCenterPoint(), gridSize, grid.GetCellDiameter());
		}

		private void DrawCell(Vector3 center, float diameter, bool isLightCell) {
			Vector3 cellSize = new Vector3(diameter, cellHeight, diameter);

			// Draw filled cell
			if (showCell)
				CulledGizmos.DrawCube(center, cellSize, isLightCell ? lightCellColor : darkCellColor);

			// Draw wireframe outline if enabled
			if (showCellWireframe)
				CulledGizmos.DrawWireCube(center, cellSize, cellWireframeColor);
		}

		private void DrawGridWireframe(Vector3 center, Vector2Int gridSize, float diameter) {
			Vector3 cellSize = new Vector3(gridSize.x * diameter, cellHeight, gridSize.y * diameter);
			CulledGizmos.DrawWireCube(center, cellSize, gridWireframeColor);
		}
	}
}
