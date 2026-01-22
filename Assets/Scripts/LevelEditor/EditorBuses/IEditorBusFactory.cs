using LevelEditor.BusGrids;

namespace LevelEditor.EditorBuses {
	public interface IEditorBusFactory {
		public EditorBus Create(EditorBus prefab, BusGrid grid, BusCell cell);
		public void Despawn(EditorBus bus);
	}
}