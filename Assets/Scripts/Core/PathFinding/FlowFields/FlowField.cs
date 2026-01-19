using Core.LevelGrids;
using Frolics.Grids.SpatialHelpers;

namespace Core.PathFinding.FlowFields {
	public class FlowField : FlowFieldBase {
		public FlowField(LevelGrid grid, SquareCoord targetCoord) : base(grid, targetCoord) { }

		protected override bool IsDestinationValid(SquareCoord targetCoord) {
			return grid.TryGetCell(targetCoord, out LevelCell cell) && IsCellWalkable(cell);
		}

		protected override bool IsNeighborValid(SquareCoord neighborCoord) {
			return grid.TryGetCell(neighborCoord, out LevelCell cell) && IsCellWalkable(cell);
		}
	}
}
