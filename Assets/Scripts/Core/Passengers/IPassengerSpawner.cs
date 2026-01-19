using Core.Data;
using Core.LevelGrids;

namespace Core.Passengers {
	public interface IPassengerSpawner {
		public void SpawnPassengers(LevelData levelData, LevelGrid grid);
	}
}
