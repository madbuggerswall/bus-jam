using Core.LevelGrids;
using Frolics.Grids.SpatialHelpers;

namespace Core.PathFinding.FlowFields {
	public class VirtualExitFlowField : FlowFieldBase {
		public VirtualExitFlowField(LevelGrid grid, SquareCoord targetCoord) : base(grid, targetCoord) { }


		protected override bool IsDestinationValid(SquareCoord targetCoord) {
			if (targetCoord.y == grid.GetGridSize().y || targetCoord.y == -1)
				return targetCoord.x >= 0 && targetCoord.x < grid.GetGridSize().x;

			if (targetCoord.x == grid.GetGridSize().x || targetCoord.x == -1)
				return targetCoord.y >= 0 && targetCoord.y < grid.GetGridSize().y;

			// Undefined behavior for VirtualExitFlowField 
			return grid.TryGetCell(targetCoord, out LevelCell cell) && IsCellWalkable(cell);
		}

		protected override bool IsNeighborValid(SquareCoord neighborCoord) {
			if (IsVirtualExit(neighborCoord))
				return true;

			bool cellFound = grid.TryGetCell(neighborCoord, out LevelCell cell);
			return cellFound && IsCellWalkable(cell);
		}

		private bool IsVirtualExit(SquareCoord coord) {
			if (targetCoord.x == grid.GetGridSize().x || targetCoord.x == -1)
				return coord.x == targetCoord.x && coord.y > -1 && coord.y < grid.GetGridSize().y;

			if (targetCoord.y == grid.GetGridSize().y || targetCoord.y == -1)
				return coord.y == targetCoord.y && coord.x > -1 && coord.x < grid.GetGridSize().x;

			return false;
		}
	}
}