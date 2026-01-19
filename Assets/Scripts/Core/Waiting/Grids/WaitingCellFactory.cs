using Frolics.Grids;
using Frolics.Grids.SpatialHelpers;

namespace Core.Waiting.Grids {
	public class WaitingCellFactory : SquareCellFactory<WaitingCell> {
		public override WaitingCell CreateCell(SquareCoord coordinate) {
			return new WaitingCell(coordinate);
		}
	}
}