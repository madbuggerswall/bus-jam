using Frolics.Pooling;
using Frolics.Utilities;
using UnityEngine;

namespace Core.LevelGrids {
	public class LevelCellBehaviourFactory : MonoBehaviour, IInitializable, ILevelCellBehaviourFactory {
		[SerializeField] private LevelCellBehaviour cellBehaviourPrefab;

		private IObjectPool<LevelCellBehaviour> cellPool;

		void IInitializable.Initialize() {
			cellPool = new ObjectPool<LevelCellBehaviour>(transform);
		}

		void ILevelCellBehaviourFactory.CreateCellBehaviours(LevelGrid grid, LevelGridBehaviour gridBehaviour) {
			LevelCell[] cells = grid.GetCells();
			for (int i = 0; i < cells.Length; i++) {
				LevelCell cell = cells[i];
				LevelCellBehaviour cellBehaviour = cellPool.Spawn(cellBehaviourPrefab, gridBehaviour.GetCellRoot());
				cellBehaviour.Initialize(cell);
				cellBehaviour.transform.position = grid.GetWorldPosition(cell);

				gridBehaviour.AddCellBehaviour(cellBehaviour);
			}
		}

		void ILevelCellBehaviourFactory.Despawn(LevelCellBehaviour cellBehaviour) {
			cellPool.Despawn(cellBehaviour);
		}
	}
}
