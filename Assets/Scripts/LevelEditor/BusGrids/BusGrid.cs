using System;
using System.Collections.Generic;
using Frolics.Grids;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace LevelEditor.BusGrids {
	public class BusGrid : SquareGrid<BusCell> {
		private readonly Transform transform;
		private readonly Vector3 pivotPoint;
		private readonly Dictionary<EditorBus, BusCell> buses = new();

		public BusGrid(
			Transform transform,
			Vector3 pivotPoint,
			Vector2Int gridSize,
			float cellDiameter,
			GridPlane gridPlane
		) : base(gridSize, cellDiameter, gridPlane, new BusCellFactory()) {
			this.transform = transform;
			this.pivotPoint = pivotPoint;
		}

		public bool CanPlaceElementAtCell(EditorBus bus, BusCell pivotCell) {
			SquareCoord[] coords = bus.GetSquareCoords();
			SquareCoord pivotCoord = pivotCell.GetCoord();

			for (int i = 0; i < coords.Length; i++)
				if (!TryGetCell(pivotCoord + coords[i], out BusCell cell) || cell.HasBus())
					return false;

			return true;
		}

		public void PlaceBusAtCell(EditorBus bus, BusCell pivotCell) {
			SquareCoord pivotCoord = pivotCell.GetCoord();
			SquareCoord[] coords = bus.GetSquareCoords();

			for (int i = 0; i < coords.Length; i++)
				if (TryGetCell(pivotCoord + coords[i], out BusCell cell))
					cell.SetBus(bus);

			if (!buses.TryAdd(bus, pivotCell))
				throw new InvalidOperationException("Element already exists");
		}

		public void RemoveBus(EditorBus bus) {
			if (!buses.Remove(bus, out BusCell pivotCell))
				throw new InvalidOperationException("EditorBus does not exist");

			RemoveBus(bus, pivotCell);
		}

		public void ClearBuses() {
			foreach ((EditorBus bus, BusCell pivotCell) in buses) {
				RemoveBus(bus, pivotCell);
				bus.GetLifecycle().Despawn();
			}
		}

		private void RemoveBus(EditorBus bus, BusCell pivotCell) {
			SquareCoord pivotCoord = pivotCell.GetCoord();
			SquareCoord[] coords = bus.GetSquareCoords();

			for (int i = 0; i < coords.Length; i++)
				if (TryGetCell(coords[i] + pivotCoord, out BusCell cell))
					cell.SetBus(null);
		}

		public override Vector3 GetPivotPoint() => transform.TransformPoint(pivotPoint);
	}
}
