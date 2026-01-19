using System.Collections.Generic;
using Core.LevelGrids;
using Frolics.Grids.SpatialHelpers;

namespace Core.PathFinding.FlowFields {
	public abstract class FlowFieldBase {
		protected readonly LevelGrid grid;
		protected readonly SquareCoord targetCoord;

		// Distance map: coord -> distance (0 = destination)
		private readonly Dictionary<SquareCoord, int> distanceMap = new();
		// Flow map: coord -> next coord to step to (towards destination)
		private readonly Dictionary<SquareCoord, SquareCoord> flowMap = new();

		protected FlowFieldBase(LevelGrid grid, SquareCoord targetCoord) {
			this.grid = grid;
			this.targetCoord = targetCoord;
		}

		// Abstract hooks for subclasses
		protected abstract bool IsDestinationValid(SquareCoord targetCoord);
		protected abstract bool IsNeighborValid(SquareCoord neighborCoord);

		// TODO SetTarget
		public void Build() {
			distanceMap.Clear();
			flowMap.Clear();
			if (!IsDestinationValid(targetCoord))
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
				if (!IsNeighborValid(neighborCoord))
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

		// Checks whether a LevelCell is walkable (not blocked by element and marked reachable).
		protected static bool IsCellWalkable(LevelCell cell) {
			return !cell.HasElement() && cell.IsReachable();
		}

		public bool IsTargetReachable(SquareCoord coord) => distanceMap.ContainsKey(coord);
	}
}