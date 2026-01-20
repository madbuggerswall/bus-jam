namespace Core.Passengers.Types {
	public class DefaultPassenger : Passenger {
		public override bool CanMove() => true;
		public override void OnNeighborMove() { }
		public override void OnAnyMove() { }
	}
}
