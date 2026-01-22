using Frolics.Grids;
using Frolics.Grids.SpatialHelpers;
using LevelEditor.EditorBuses;

namespace LevelEditor.BusGrids {
	public class BusCell : SquareCell {
		private EditorBus bus;

		public BusCell(SquareCoord coordinate) : base(coordinate) { }

		public bool HasBus() => bus is not null;

		// Getters
		public EditorBus GetBus() => bus;

		// Setters
		public void SetBus(EditorBus bus) => this.bus = bus;
	}
}
