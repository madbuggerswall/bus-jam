using Frolics.Pooling;
using Frolics.Utilities;
using UnityEngine;

namespace LevelEditor.BusGrids {
	public class BusCellBehaviourFactory : MonoBehaviour, IInitializable, IBusCellBehaviourFactory {
		[SerializeField] private BusCellBehaviour cellBehaviourPrefab;

		private IObjectPool<BusCellBehaviour> cellPool;

		void IInitializable.Initialize() {
			cellPool = new ObjectPool<BusCellBehaviour>(transform);
		}

		void IBusCellBehaviourFactory.CreateCellBehaviours(BusGrid grid, BusGridBehaviour gridBehaviour) {
			BusCell[] cells = grid.GetCells();
			for (int i = 0; i < cells.Length; i++) {
				BusCell cell = cells[i];
				BusCellBehaviour cellBehaviour = cellPool.Spawn(cellBehaviourPrefab, gridBehaviour.GetCellRoot());
				cellBehaviour.Initialize(cell);
				cellBehaviour.transform.position = grid.GetWorldPosition(cell);

				gridBehaviour.AddCellBehaviour(cellBehaviour);
			}
		}

		void IBusCellBehaviourFactory.Despawn(BusCellBehaviour cellBehaviour) {
			cellPool.Despawn(cellBehaviour);
		}
	}
}
