using Core.Data;
using LevelEditor.BusGrids;

namespace LevelEditor {
	public interface IBusDTOSpawner {
		public void SpawnBus(BusDTO busDTO, BusCell cell);
	}
}