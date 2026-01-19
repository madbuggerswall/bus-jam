using Core.Passengers;
using Frolics.Grids;
using Frolics.Grids.SpatialHelpers;

namespace Core.Waiting.Grids {
	public class WaitingCell : SquareCell {
		private Passenger passenger;

		public WaitingCell(SquareCoord coordinate) : base(coordinate) { }

		public bool HasPassenger() => passenger is not null;

		// Getters
		public Passenger GetPassenger() => passenger;

		// Setters
		public void SetPassenger(Passenger passenger) => this.passenger = passenger;
	}
}
