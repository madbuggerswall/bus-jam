using Core.LevelGrids;
using Core.Passengers;

namespace Core.Mechanics {
	public interface ILevelAreaManager {
		public void RemovePassenger(Passenger passenger);
		public LevelCell GetCell(Passenger passenger);
	}
}