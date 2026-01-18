using Core.GridElements;
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
			// TODO Set element's parent to an elementRoot 
			GridElement element = pool.Spawn(prefab);
			ElementLifecycle lifecycle = new(element, grid, pool);
			element.Initialize(lifecycle);

			Vector3 pivotOffset = element.transform.position - element.GetPivotWorldPosition();
			element.transform.position = grid.GetWorldPosition(cell) + pivotOffset;

			grid.PlaceElementAtCell(cell, element);
			return element;
		}
	}
}
