using System;
using System.Collections.Generic;
using Frolics.Grids.SpatialHelpers;
using Frolics.Utilities;
using UnityEngine;

// Place this in an appropriate namespace (e.g., Core.Pathfinding)
namespace Core.PathFinding {
	public interface IFlowFieldManager {
		public void OnGridModified();
		public List<SquareCoord> GetPath(SquareCoord sourceCoord);
	}

	public class FlowFieldManager : IInitializable, IFlowFieldManager {
		private VirtualExitFlowField flowField;
		private LevelGrid levelGrid;

		void IInitializable.Initialize() {
			flowField = new VirtualExitFlowField(
				levelGrid,
				VirtualExitFlowField.Edge.Forward,
				levelGrid.GetGridSize().x / 2
			);
		}

		void IFlowFieldManager.OnGridModified() {
			flowField.Build();
		}

		List<SquareCoord> IFlowFieldManager.GetPath(SquareCoord sourceCoord) {
			return flowField.GetPath(sourceCoord);
		}
	}

	public class FlowField {
		private readonly LevelGrid grid;

		// Distance map: coord -> distance (0 = destination)
		private readonly Dictionary<SquareCoord, int> distanceMap = new();

		// Flow map: coord -> next coord to step to (towards destination)
		private readonly Dictionary<SquareCoord, SquareCoord> flowMap = new();

		public FlowField(LevelGrid grid) {
			this.grid = grid ?? throw new ArgumentNullException(nameof(grid));
		}

		// Build distance + flow fields from the given destination coordinate.
		public void Build(SquareCoord targetCoord) {
			distanceMap.Clear();
			flowMap.Clear();

			// Destination must exist and be walkable
			// Nothing reachable if destination is invalid/unwalkable
			if (!grid.TryGetCell(targetCoord, out LevelCell targetCell) || !IsCellWalkable(targetCell))
				return;

			BuildDistanceMap(targetCoord);
			BuildFlowMap(targetCoord);
		}

		private void BuildDistanceMap(SquareCoord targetCoord) {
			distanceMap[targetCoord] = 0;

			Queue<SquareCoord> frontier = new();
			frontier.Enqueue(targetCoord);

			while (frontier.Count > 0)
				ExpandFrontierNode(frontier);
		}

		// BuildDistanceMap helper
		private void ExpandFrontierNode(Queue<SquareCoord> frontier) {
			SquareCoord current = frontier.Dequeue();
			int currentDistance = distanceMap[current];

			for (int i = 0; i < SquareCoord.DirectionVectors.Length; i++) {
				SquareCoord neighborCoord = current + SquareCoord.DirectionVectors[i];

				// Skip if already visited
				if (distanceMap.ContainsKey(neighborCoord))
					continue;

				// Skip if cell doesn't exist or is not walkable
				if (!grid.TryGetCell(neighborCoord, out LevelCell neighborCell) || !IsCellWalkable(neighborCell))
					continue;

				distanceMap[neighborCoord] = currentDistance + 1;
				frontier.Enqueue(neighborCoord);
			}
		}

		// Build flowMap: for each reachable coord, pick neighbor with the smallest distance
		private void BuildFlowMap(SquareCoord targetCoord) {
			foreach ((SquareCoord coord, _) in distanceMap)
				AssignFlowDirection(targetCoord, coord);
		}

		// BuildFlowMap helper: Computes flow for coord
		private void AssignFlowDirection(SquareCoord targetCoord, SquareCoord coord) {
			// Destination flows to itself
			if (coord.Equals(targetCoord)) {
				flowMap[coord] = coord;
				return;
			}

			SquareCoord best = coord;
			int shortestDistance = int.MaxValue;

			for (int i = 0; i < SquareCoord.DirectionVectors.Length; i++) {
				SquareCoord neighborCoord = coord + SquareCoord.DirectionVectors[i];
				if (!distanceMap.TryGetValue(neighborCoord, out int distance) || distance >= shortestDistance)
					continue;

				shortestDistance = distance;
				best = neighborCoord;
			}

			// If no neighbor has a smaller distance, the cell remains without flow (isolated)
			if (shortestDistance < int.MaxValue)
				flowMap[coord] = best;
		}

		// Returns a path of SquareCoord from source to destination following the flow.
		// If source is unreachable or flow not built, returns an empty list.
		public List<SquareCoord> GetPath(SquareCoord sourceCoord, int maxStepsMultiplier = 4) {
			List<SquareCoord> path = new();

			// unreachable
			if (!distanceMap.ContainsKey(sourceCoord))
				return path;

			int maxSteps = grid.GetGridSize().x * grid.GetGridSize().y * maxStepsMultiplier;
			SquareCoord currentCoord = sourceCoord;
			for (int i = 0; i < maxSteps; i++) {
				path.Add(currentCoord);

				if (!flowMap.TryGetValue(currentCoord, out SquareCoord nextCoord))
					break;

				// Reached destination (flow to self)
				// Prevent infinite loops: if next already in path, break
				if (nextCoord.Equals(currentCoord) || path.Contains(nextCoord))
					break;

				currentCoord = nextCoord;
			}

			return path;
		}

		/// <summary>
		/// Helper: checks whether a LevelCell is walkable (not blocked by element and marked reachable).
		/// </summary>
		private static bool IsCellWalkable(LevelCell cell) {
			return !cell.HasElement() && cell.IsReachable();
		}

		public bool IsReachable(SquareCoord coord) => distanceMap.ContainsKey(coord);
	}

	public class VirtualExitFlowField {
		public enum Edge { Forward, Back, Left, Right }

		private readonly LevelGrid grid;
		private readonly SquareCoord exitCoord;
		private readonly Edge edge;

		// Distance map: coord -> distance (0 = destination)
		private readonly Dictionary<SquareCoord, int> distanceMap = new();

		// Flow map: coord -> next coord to step to (towards destination)
		private readonly Dictionary<SquareCoord, SquareCoord> flowMap = new();


		public VirtualExitFlowField(LevelGrid grid, Edge edge, int edgeCoord) {
			this.grid = grid;
			this.edge = edge;
			this.exitCoord = GetExitCoord(edge, edgeCoord);
		}

		private SquareCoord GetExitCoord(Edge edge, int edgeCoord) {
			int clamped = ClampEdgeCoord(edge, edgeCoord);
			Vector2Int size = grid.GetGridSize();

			return edge switch {
				Edge.Forward => new SquareCoord(clamped, size.y), // one row beyond top
				Edge.Back => new SquareCoord(clamped, -1),        // one row before bottom
				Edge.Left => new SquareCoord(-1, clamped),        // one col before left
				Edge.Right => new SquareCoord(size.x, clamped),   // one col beyond right
				_ => throw new ArgumentOutOfRangeException(nameof(edge), edge, null)
			};
		}

		private int ClampEdgeCoord(Edge edge, int edgeCoord) {
			var size = grid.GetGridSize();
			return edge switch {
				Edge.Forward or Edge.Back => Mathf.Clamp(edgeCoord, 0, size.x - 1),
				Edge.Left or Edge.Right => Mathf.Clamp(edgeCoord, 0, size.y - 1),
				_ => throw new ArgumentOutOfRangeException(nameof(edge), edge, null)
			};
		}

		public void Build() {
			distanceMap.Clear();
			flowMap.Clear();

			BuildDistanceMap(exitCoord);
			BuildFlowMap(exitCoord);
		}

		private void BuildDistanceMap(SquareCoord targetCoord) {
			distanceMap[targetCoord] = 0;

			Queue<SquareCoord> frontier = new();
			frontier.Enqueue(targetCoord);

			while (frontier.Count > 0)
				ExpandFrontierNode(frontier);
		}

		// BuildDistanceMap helper
		private void ExpandFrontierNode(Queue<SquareCoord> frontier) {
			SquareCoord current = frontier.Dequeue();
			int currentDistance = distanceMap[current];

			for (int i = 0; i < SquareCoord.DirectionVectors.Length; i++) {
				SquareCoord neighborCoord = current + SquareCoord.DirectionVectors[i];

				// Skip if already visited
				if (distanceMap.ContainsKey(neighborCoord))
					continue;

				// Allow virtual exit row/col
				if (IsVirtualExit(neighborCoord)) {
					distanceMap[neighborCoord] = currentDistance + 1;
					frontier.Enqueue(neighborCoord);
					continue;
				}

				// Skip if cell doesn't exist or is not walkable
				if (!grid.TryGetCell(neighborCoord, out LevelCell neighborCell) || !IsCellWalkable(neighborCell))
					continue;

				distanceMap[neighborCoord] = currentDistance + 1;
				frontier.Enqueue(neighborCoord);
			}
		}

		// Build flowMap: for each reachable coord, pick neighbor with the smallest distance
		private void BuildFlowMap(SquareCoord targetCoord) {
			foreach ((SquareCoord coord, _) in distanceMap)
				AssignFlowDirection(targetCoord, coord);
		}

		// BuildFlowMap helper: Computes flow for coord
		private void AssignFlowDirection(SquareCoord targetCoord, SquareCoord coord) {
			// Destination flows to itself
			if (coord.Equals(targetCoord)) {
				flowMap[coord] = coord;
				return;
			}

			SquareCoord best = coord;
			int shortestDistance = int.MaxValue;

			for (int i = 0; i < SquareCoord.DirectionVectors.Length; i++) {
				SquareCoord neighborCoord = coord + SquareCoord.DirectionVectors[i];
				if (!distanceMap.TryGetValue(neighborCoord, out int distance) || distance >= shortestDistance)
					continue;

				shortestDistance = distance;
				best = neighborCoord;
			}

			// If no neighbor has a smaller distance, the cell remains without flow (isolated)
			if (shortestDistance < int.MaxValue)
				flowMap[coord] = best;
		}

		private bool IsVirtualExit(SquareCoord coord) {
			var size = grid.GetGridSize();
			return edge switch {
				Edge.Forward => coord.y == size.y,
				Edge.Back => coord.y == -1,
				Edge.Left => coord.x == -1,
				Edge.Right => coord.x == size.x,
				_ => false
			};
		}

		public List<SquareCoord> GetPath(SquareCoord sourceCoord, int maxStepsMultiplier = 4) {
			List<SquareCoord> path = new();
			if (!distanceMap.ContainsKey(sourceCoord))
				return path;

			int maxSteps = grid.GetGridSize().x * grid.GetGridSize().y * maxStepsMultiplier;
			SquareCoord currentCoord = sourceCoord;

			for (int i = 0; i < maxSteps; i++) {
				path.Add(currentCoord);

				if (!flowMap.TryGetValue(currentCoord, out SquareCoord nextCoord))
					break;

				if (nextCoord == currentCoord || path.Contains(nextCoord))
					break;

				currentCoord = nextCoord;
			}

			return path;
		}

		private static bool IsCellWalkable(LevelCell cell) => !cell.HasElement() && cell.IsReachable();

		public bool IsReachable(SquareCoord coord) => distanceMap.ContainsKey(coord);
	}
}
