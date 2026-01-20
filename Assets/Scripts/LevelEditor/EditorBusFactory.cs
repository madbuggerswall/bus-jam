using Frolics.Pooling;
using Frolics.Utilities;
using LevelEditor.BusGrids;
using UnityEngine;

namespace LevelEditor {
	public interface IEditorBusFactory {
		public EditorBus Create(EditorBus prefab, BusGrid grid, BusCell cell);
		public void Despawn(EditorBus bus);
	}

	public class EditorBusFactory : MonoBehaviour, IInitializable, IEditorBusFactory {
		private IObjectPool<EditorBus> pool;

		void IInitializable.Initialize() {
			pool = new ObjectPool<EditorBus>(transform);
		}

		EditorBus IEditorBusFactory.Create(EditorBus prefab, BusGrid grid, BusCell cell) {
			EditorBus element = pool.Spawn(prefab);

			Vector3 pivotOffset = element.transform.position - element.GetPivotWorldPosition();
			element.transform.position = grid.GetWorldPosition(cell) + pivotOffset;

			grid.PlaceBusAtCell(element, cell);
			return element;
		}

		void IEditorBusFactory.Despawn(EditorBus bus) {
			pool.Despawn(bus);
		}
	}
}
