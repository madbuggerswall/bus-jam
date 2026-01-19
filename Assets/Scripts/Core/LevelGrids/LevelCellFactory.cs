using Frolics.Grids;
using Frolics.Grids.SpatialHelpers;

namespace Core.LevelGrids {
	public class LevelCellFactory : SquareCellFactory<LevelCell> {
		public override LevelCell CreateCell(SquareCoord coordinate) {
			return new LevelCell(coordinate);
		}
	}
}
