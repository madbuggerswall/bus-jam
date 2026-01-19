using Core.GridElements;
using Core.LevelGrids;

namespace Core.Passengers {
	public interface IGridElementFactory {
		public GridElement Create(GridElement prefab, LevelGrid grid, LevelCell cell);
	}
}