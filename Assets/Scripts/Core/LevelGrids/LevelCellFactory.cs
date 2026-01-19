using Core.Waiting.Grids;
using Frolics.Grids;
using Frolics.Grids.SpatialHelpers;

namespace Core.LevelGrids {
	public class LevelCellFactory : SquareCellFactory<LevelCell> {
		public override LevelCell CreateCell(SquareCoord coordinate) {
			return new LevelCell(coordinate);
		}
	}

	public class WaitingCellFactory : SquareCellFactory<WaitingCell> {
		public override WaitingCell CreateCell(SquareCoord coordinate) {
			return new WaitingCell(coordinate);
		}
	}
}
