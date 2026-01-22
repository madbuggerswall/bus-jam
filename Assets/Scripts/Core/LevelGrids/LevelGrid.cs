using System;
using System.Collections.Generic;
using Core.GridElements;
using Frolics.Grids;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Core.LevelGrids {
	public class LevelGrid : SquareGrid<LevelCell> {
		private readonly Transform transform;
		private readonly Vector3 pivotPoint;
		private readonly Dictionary<GridElement, LevelCell> elements = new();

		public LevelGrid(
			Transform transform,
			Vector3 pivotPoint,
			Vector2Int gridSize,
			float cellDiameter,
			GridPlane gridPlane
		) : base(gridSize, cellDiameter, gridPlane, new LevelCellFactory()) {
			this.transform = transform;
			this.pivotPoint = pivotPoint;
		}

		public void PlaceElementAtCell(GridElement element, LevelCell pivotCell) {
			SquareCoord pivotCoord = pivotCell.GetCoord();
			SquareCoord[] coords = element.GetSquareCoords();

			for (int i = 0; i < coords.Length; i++)
				if (TryGetCell(pivotCoord + coords[i], out LevelCell cell))
					cell.SetGridElement(element);

			if (!elements.TryAdd(element, pivotCell))
				throw new InvalidOperationException("Element already exists");
		}

		public void RemoveElement(GridElement element) {
			if (!elements.Remove(element, out LevelCell pivotCell))
				throw new InvalidOperationException("Element does not exist");

			RemoveElement(element, pivotCell);
		}

		public LevelCell GetCell(GridElement element) {
			if (!elements.TryGetValue(element, out LevelCell cell))
				throw new InvalidOperationException("Element does not exist");

			return cell;
		}

		private void RemoveElement(GridElement element, LevelCell pivotCell) {
			SquareCoord pivotCoord = pivotCell.GetCoord();
			SquareCoord[] coords = element.GetSquareCoords();

			for (int i = 0; i < coords.Length; i++)
				if (TryGetCell(coords[i] + pivotCoord, out LevelCell cell))
					cell.SetGridElement(null);
		}

		// Editor
		public Dictionary<GridElement, LevelCell> GetElements() => elements;

		public override Vector3 GetPivotPoint() => transform.TransformPoint(pivotPoint);
	}
}
