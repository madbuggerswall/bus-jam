using Core.Data;

namespace Core.Passengers {
	public interface IPassengerSpawner {
		public Passenger Spawn(PassengerType type, LevelGrid grid, LevelCell cell);
	}
}