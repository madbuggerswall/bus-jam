using Core.GridElements;

namespace Core.Passengers {
	public interface IGridElementFactory {
		public GridElement Create(GridElement prefab, LevelGrid grid, LevelCell cell);
	}
}