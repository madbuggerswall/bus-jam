using Frolics.Pooling;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Waiting.Grids {
	public class WaitingCellBehaviourFactory : MonoBehaviour, IInitializable, IWaitingCellBehaviourFactory {
		[SerializeField] private WaitingCellBehaviour cellBehaviourPrefab;

		private IObjectPool<WaitingCellBehaviour> cellPool;

		void IInitializable.Initialize() {
			cellPool = new ObjectPool<WaitingCellBehaviour>(transform);
		}

		void IWaitingCellBehaviourFactory.CreateCellBehaviours(WaitingGrid grid, WaitingGridBehaviour gridBehaviour) {
			WaitingCell[] cells = grid.GetCells();
			for (int i = 0; i < cells.Length; i++) {
				WaitingCell cell = cells[i];
				WaitingCellBehaviour cellBehaviour = cellPool.Spawn(cellBehaviourPrefab, gridBehaviour.GetCellRoot());
				cellBehaviour.Initialize(cell);
				cellBehaviour.transform.position = grid.GetWorldPosition(cell);

				gridBehaviour.AddCellBehaviour(cellBehaviour);
			}
		}

		void IWaitingCellBehaviourFactory.Despawn(WaitingCellBehaviour cellBehaviour) {
			cellPool.Despawn(cellBehaviour);
		}
	}
}
