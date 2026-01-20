using System;
using System.Collections.Generic;
using Core.Passengers;
using Frolics.Grids;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Core.Waiting.Grids {
	public class WaitingGrid : SquareGrid<WaitingCell> {
		private readonly Transform transform;
		private readonly Vector3 pivotPoint;
		private readonly Dictionary<Passenger, WaitingCell> passengers = new();

		public WaitingGrid(
			Transform transform,
			Vector3 pivotPoint,
			Vector2Int gridSize,
			float cellDiameter,
			GridPlane gridPlane
		) : base(gridSize, cellDiameter, gridPlane, new WaitingCellFactory()) {
			this.transform = transform;
			this.pivotPoint = pivotPoint;
		}

		public bool CanPlaceElementAtCell(Passenger passenger, WaitingCell pivotCell) {
			SquareCoord[] coords = passenger.GetSquareCoords();
			SquareCoord pivotCoord = pivotCell.GetCoord();

			for (int i = 0; i < coords.Length; i++)
				if (!TryGetCell(pivotCoord + coords[i], out WaitingCell cell) || cell.HasPassenger())
					return false;

			return true;
		}

		public void PlacePassengerAtCell(Passenger passenger, WaitingCell pivotCell) {
			SquareCoord pivotCoord = pivotCell.GetCoord();
			SquareCoord[] coords = passenger.GetSquareCoords();

			for (int i = 0; i < coords.Length; i++)
				if (TryGetCell(pivotCoord + coords[i], out WaitingCell cell))
					cell.SetPassenger(passenger);

			if (!passengers.TryAdd(passenger, pivotCell))
				throw new InvalidOperationException("Element already exists");
		}

		public void RemovePassenger(Passenger passenger) {
			if (!passengers.Remove(passenger, out WaitingCell pivotCell))
				throw new InvalidOperationException("Passenger does not exist");

			RemovePassenger(passenger, pivotCell);
		}

		public void ClearPassengers() {
			foreach ((Passenger passenger, WaitingCell pivotCell) in passengers) {
				RemovePassenger(passenger, pivotCell);
				passenger.GetLifecycle().Despawn();
			}
		}

		private void RemovePassenger(Passenger passenger, WaitingCell pivotCell) {
			SquareCoord pivotCoord = pivotCell.GetCoord();
			SquareCoord[] coords = passenger.GetSquareCoords();

			for (int i = 0; i < coords.Length; i++)
				if (TryGetCell(coords[i] + pivotCoord, out WaitingCell cell))
					cell.SetPassenger(null);
		}

		public override Vector3 GetPivotPoint() => transform.TransformPoint(pivotPoint);
	}
}
