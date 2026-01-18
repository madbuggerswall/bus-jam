using System;
using System.Collections.Generic;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

// Place this in an appropriate namespace (e.g., Core.Pathfinding)
namespace Core.PathFinding {
	public class FlowField {
		private readonly LevelGrid grid;

		// Distance map: coord -> distance (0 = destination)
		private readonly Dictionary<SquareCoord, int> distanceMap = new();

		// Flow map: coord -> next coord to step to (towards destination)
		private readonly Dictionary<SquareCoord, SquareCoord> flowMap = new();

		public FlowField(LevelGrid grid) {
			this.grid = grid ?? throw new ArgumentNullException(nameof(grid));
		}

		/// <summary>
		/// Build distance + flow fields from the given destination coordinate.
		/// Cells with HasElement == true or IsReachable == false are treated as obstacles.
		/// </summary>
		public void Build(SquareCoord destination) {
			distanceMap.Clear();
			flowMap.Clear();

			// Destination must exist and be walkable
			if (!grid.TryGetCell(destination, out LevelCell destCell) || !IsCellWalkable(destCell)) {
				// Nothing reachable if destination is invalid/unwalkable
				return;
			}

			distanceMap[destination] = 0;
			Queue<SquareCoord> frontier = new();
			frontier.Enqueue(destination);

			// Build distanceMap: to compute distances
			while (frontier.Count > 0) {
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
			foreach ((SquareCoord coord, _) in distanceMap) {
				// Destination flows to itself
				if (coord.Equals(destination)) {
					flowMap[coord] = coord;
					continue;
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

				if (shortestDistance < int.MaxValue) {
					flowMap[coord] = best;
				}
				// If no neighbor has a smaller distance, the cell remains without flow (isolated)
			}
		}

		/// <summary>
		/// Returns a path of SquareCoord from source to destination following the flow.
		/// If source is unreachable or flow not built, returns an empty list.
		/// </summary>
		public List<SquareCoord> GetPath(SquareCoord sourceCoord, int maxSteps = 10000) {
			List<SquareCoord> path = new();

			// unreachable
			if (!distanceMap.ContainsKey(sourceCoord))
				return path; 

			SquareCoord currentCoord = sourceCoord;
			for (int i = 0; i < maxSteps; i++) {
				path.Add(currentCoord);

				if (!flowMap.TryGetValue(currentCoord, out SquareCoord nextCoord))
					break;

				// Reached destination (flow to self)
				if (nextCoord.Equals(currentCoord))
					break;

				// Prevent infinite loops: if next already in path, break
				if (path.Contains(nextCoord))
					break;

				currentCoord = nextCoord;
			}

			return path;
		}

		/// <summary>
		/// Returns world-space positions for the path (useful for steering).
		/// </summary>
		public List<Vector3> GetWorldPath(SquareCoord source, int maxSteps = 10000) {
			List<SquareCoord> coordPath = GetPath(source, maxSteps);
			List<Vector3> worldPath = new List<Vector3>(coordPath.Count);

			foreach (SquareCoord coord in coordPath) {
				if (grid.TryGetCell(coord, out LevelCell cell)) {
					worldPath.Add(grid.GetWorldPosition(cell));
				}
			}

			return worldPath;
		}

		/// <summary>
		/// Helper: checks whether a LevelCell is walkable (not blocked by element and marked reachable).
		/// </summary>
		private static bool IsCellWalkable(LevelCell cell) {
			return !cell.HasElement() && cell.IsReachable();
		}

		/// <summary>
		/// Optional helpers to query internal maps
		/// </summary>
		public bool IsReachable(SquareCoord coord) => distanceMap.ContainsKey(coord);

		public bool TryGetDistance(SquareCoord coord, out int dist) => distanceMap.TryGetValue(coord, out dist);
		public bool TryGetFlow(SquareCoord coord, out SquareCoord next) => flowMap.TryGetValue(coord, out next);
	}
}
