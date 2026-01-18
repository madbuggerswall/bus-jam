using System;
using System.Collections.Generic;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

// Place this in an appropriate namespace (e.g., Core.Pathfinding)
public class LevelFlowField {
    private readonly LevelGrid grid;

    // Distance map: coord -> distance (0 = destination)
    private readonly Dictionary<SquareCoord, int> distance = new();

    // Flow map: coord -> next coord to step to (towards destination)
    private readonly Dictionary<SquareCoord, SquareCoord> flow = new();

    public LevelFlowField(LevelGrid grid) {
        this.grid = grid ?? throw new ArgumentNullException(nameof(grid));
    }

    /// <summary>
    /// Build distance + flow fields from the given destination coordinate.
    /// Cells with HasElement == true or IsReachable == false are treated as obstacles.
    /// </summary>
    public void Build(SquareCoord destination) {
        distance.Clear();
        flow.Clear();

        // Destination must exist and be walkable
        if (!grid.TryGetCell(destination, out LevelCell destCell) || !IsCellWalkable(destCell)) {
            // Nothing reachable if destination is invalid/unwalkable
            return;
        }

        var q = new Queue<SquareCoord>();
        distance[destination] = 0;
        q.Enqueue(destination);

        // BFS to compute distances
        while (q.Count > 0) {
            var cur = q.Dequeue();
            int curDist = distance[cur];

            foreach (var nb in GetNeighbors(cur)) {
                // Skip if already visited
                if (distance.ContainsKey(nb)) continue;

                // Skip if cell doesn't exist or is not walkable
                if (!grid.TryGetCell(nb, out LevelCell nbCell) || !IsCellWalkable(nbCell)) continue;

                distance[nb] = curDist + 1;
                q.Enqueue(nb);
            }
        }

        // Build flow: for each reachable coord, pick neighbor with smallest distance
        foreach (var kv in distance) {
            var coord = kv.Key;

            // Destination flows to itself
            if (coord.Equals(destination)) {
                flow[coord] = coord;
                continue;
            }

            SquareCoord best = coord;
            int bestDist = int.MaxValue;

            foreach (var nb in GetNeighbors(coord)) {
                if (distance.TryGetValue(nb, out int d) && d < bestDist) {
                    bestDist = d;
                    best = nb;
                }
            }

            if (bestDist < int.MaxValue) {
                flow[coord] = best;
            }
            // If no neighbor has a smaller distance, the cell remains without flow (isolated)
        }
    }

    /// <summary>
    /// Returns a path of SquareCoord from source to destination following the flow.
    /// If source is unreachable or flow not built, returns an empty list.
    /// </summary>
    public List<SquareCoord> GetPath(SquareCoord source, int maxSteps = 10000) {
        var path = new List<SquareCoord>();

        if (!distance.ContainsKey(source)) return path; // unreachable

        var cur = source;
        for (int i = 0; i < maxSteps; i++) {
            path.Add(cur);

            if (!flow.TryGetValue(cur, out var next)) break;

            // Reached destination (flow to self)
            if (next.Equals(cur)) break;

            // Prevent infinite loops: if next already in path, break
            if (path.Contains(next)) break;

            cur = next;
        }

        return path;
    }

    /// <summary>
    /// Returns world-space positions for the path (useful for steering).
    /// </summary>
    public List<Vector3> GetWorldPath(SquareCoord source, int maxSteps = 10000) {
        var coordPath = GetPath(source, maxSteps);
        var worldPath = new List<Vector3>(coordPath.Count);

        foreach (var coord in coordPath) {
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
        if (cell == null) return false;
        if (cell.HasElement()) return false;
        if (!cell.IsReachable()) return false;
        return true;
    }

    /// <summary>
    /// Neighbor generator for square grid using SquareCoord.DirectionVectors (8 directions).
    /// If you prefer 4-way movement, filter diagonals here.
    /// </summary>
    private IEnumerable<SquareCoord> GetNeighbors(SquareCoord coord) {
        foreach (var v in SquareCoord.DirectionVectors) {
            yield return coord + v;
        }
    }

    /// <summary>
    /// Optional helpers to query internal maps
    /// </summary>
    public bool IsReachable(SquareCoord coord) => distance.ContainsKey(coord);
    public bool TryGetDistance(SquareCoord coord, out int dist) => distance.TryGetValue(coord, out dist);
    public bool TryGetFlow(SquareCoord coord, out SquareCoord next) => flow.TryGetValue(coord, out next);
}
