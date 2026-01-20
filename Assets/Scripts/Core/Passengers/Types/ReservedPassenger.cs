namespace Core.Passengers.Types {
	public class ReservedPassenger : Passenger {
		public override bool CanMove() => true;
		public override void OnNeighborMove() { }
		public override void OnAnyMove() { }
	}
}
