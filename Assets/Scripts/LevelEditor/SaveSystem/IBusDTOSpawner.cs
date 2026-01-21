using Core.Data;
using LevelEditor.BusGrids;

namespace LevelEditor.SaveSystem {
	public interface IBusDTOSpawner {
		public void SpawnBus(BusDTO busDTO, BusCell cell);
	}
}