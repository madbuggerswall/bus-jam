using System;
using System.Collections;
using System.Collections.Generic;
using Core.GridElements;
using Core.LevelGrids;
using Frolics.Grids;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

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

	public bool CanPlaceElementAtCell(LevelCell pivotCell, GridElement element) {
		SquareCoord[] coords = element.GetSquareCoords();
		SquareCoord pivotCoord = pivotCell.GetCoord();

		for (int i = 0; i < coords.Length; i++)
			if (!TryGetCell(pivotCoord + coords[i], out LevelCell cell) || cell.HasElement())
				return false;

		return true;
	}

	public void PlaceElementAtCell(LevelCell pivotCell, GridElement element) {
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

	public void ClearElements() {
		foreach ((GridElement element, LevelCell pivotCell) in elements) {
			RemoveElement(element, pivotCell);
			element.GetLifecycle().Despawn();
		}
	}

	private void RemoveElement(GridElement element, LevelCell pivotCell) {
		SquareCoord pivotCoord = pivotCell.GetCoord();
		SquareCoord[] coords = element.GetSquareCoords();

		for (int i = 0; i < coords.Length; i++)
			if (TryGetCell(coords[i] + pivotCoord, out LevelCell cell))
				cell.SetGridElement(null);
	}

	public override Vector3 GetPivotPoint() => transform.TransformPoint(pivotPoint);
}
