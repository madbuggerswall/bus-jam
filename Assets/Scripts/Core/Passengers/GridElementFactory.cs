using Core.GridElements;
using Core.LevelGrids;
using Frolics.Pooling;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Passengers {
	public class GridElementFactory : MonoBehaviour, IInitializable, IGridElementFactory {
		private IObjectPool<GridElement> pool;

		void IInitializable.Initialize() {
			pool = new ObjectPool<GridElement>(transform);
		}

		GridElement IGridElementFactory.Create(GridElement prefab, LevelGrid grid, LevelCell cell) {
			GridElement element = pool.Spawn(prefab);

			Vector3 pivotOffset = element.transform.position - element.GetPivotWorldPosition();
			element.transform.position = grid.GetWorldPosition(cell) + pivotOffset;

			grid.PlaceElementAtCell(element, cell);
			return element;
		}

		void IGridElementFactory.Despawn(GridElement element) {
			pool.Despawn(element);
		}
	}
}
