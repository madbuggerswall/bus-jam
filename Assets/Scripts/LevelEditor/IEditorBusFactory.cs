using LevelEditor.BusGrids;

namespace LevelEditor {
	public interface IEditorBusFactory {
		public EditorBus Create(EditorBus prefab, BusGrid grid, BusCell cell);
		public void Despawn(EditorBus bus);
	}
}