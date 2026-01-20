using Core.Passengers;

namespace Core.LevelGrids {
	public interface ILevelAreaManager {
		public void RemovePassenger(Passenger passenger);
		public LevelCell GetCell(Passenger passenger);
	}
}