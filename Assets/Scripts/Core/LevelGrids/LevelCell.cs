using Core.GridElements;
using Frolics.Grids;
using Frolics.Grids.SpatialHelpers;

namespace Core.LevelGrids {
	public class LevelCell : SquareCell {
		private GridElement element;
		private bool isReachable;

		public LevelCell(SquareCoord coordinate) : base(coordinate) { }


		public bool IsReachable() => isReachable;
		public bool HasElement() => element is not null;

		// Getters
		public GridElement GetGridElement() => element;

		// Setters
		public void SetReachable(bool isReachable) => this.isReachable = isReachable;
		public void SetGridElement(GridElement element) => this.element = element;
	}
}
