using Core.LevelGrids;

namespace Core.GridElements {
	public interface IGridElementFactory {
		public GridElement Create(GridElement prefab, LevelGrid grid, LevelCell cell);
		public void Despawn(GridElement element);
	}
}
