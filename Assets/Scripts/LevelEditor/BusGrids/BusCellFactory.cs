using Frolics.Grids;
using Frolics.Grids.SpatialHelpers;

namespace LevelEditor.BusGrids {
	public class BusCellFactory : SquareCellFactory<BusCell> {
		public override BusCell CreateCell(SquareCoord coordinate) {
			return new BusCell(coordinate);
		}
	}
}
