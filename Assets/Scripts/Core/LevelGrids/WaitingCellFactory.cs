using Core.Waiting.Grids;
using Frolics.Grids;
using Frolics.Grids.SpatialHelpers;

namespace Core.LevelGrids {
	public class WaitingCellFactory : SquareCellFactory<WaitingCell> {
		public override WaitingCell CreateCell(SquareCoord coordinate) {
			return new WaitingCell(coordinate);
		}
	}
}