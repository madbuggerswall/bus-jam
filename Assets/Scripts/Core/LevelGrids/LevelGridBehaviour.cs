using Core.Data;
using Frolics.Grids;
using Frolics.Pooling;
using Frolics.Utilities;
using UnityEngine;

namespace Core.LevelGrids {
	public interface ILevelGridBehaviourFactory {
		public LevelGridBehaviour Create(Vector2Int gridSize, GridPlane gridPlane);
	}

	public interface ILevelCellBehaviourFactory {
		public void CreateCellBehaviours(LevelGrid grid, LevelGridBehaviour gridBehaviour);
	}

	public class LevelCellBehaviourFactory : MonoBehaviour, IInitializable, ILevelCellBehaviourFactory {
		[SerializeField] private Transform cellPoolRoot;
		[SerializeField] private LevelCellBehaviour cellBehaviourPrefab;

		private IObjectPool<LevelCellBehaviour> cellPool;

		void IInitializable.Initialize() {
			cellPool = new ObjectPool<LevelCellBehaviour>(cellPoolRoot);
		}

		void ILevelCellBehaviourFactory.CreateCellBehaviours(LevelGrid grid, LevelGridBehaviour gridBehaviour) {
			LevelCell[] cells = grid.GetCells();
			for (int i = 0; i < cells.Length; i++) {
				LevelCell cell = cells[i];
				LevelCellBehaviour cellBehaviour = cellPool.Spawn(cellBehaviourPrefab, gridBehaviour.GetCellRoot());
				cellBehaviour.Initialize(cell);
				cellBehaviour.transform.position = grid.GetWorldPosition(cell);
			}
		}
	}

	public class LevelGridBehaviourFactory : MonoBehaviour, IInitializable, ILevelGridBehaviourFactory {
		[SerializeField] private Transform gridRoot;
		[SerializeField] private Transform gridPoolRoot;

		[SerializeField] private LevelGridBehaviour gridBehaviourPrefab;

		private IObjectPool<LevelGridBehaviour> gridPool;

		void IInitializable.Initialize() {
			gridPool = new ObjectPool<LevelGridBehaviour>(gridPoolRoot);
		}

		LevelGridBehaviour ILevelGridBehaviourFactory.Create(Vector2Int gridSize, GridPlane gridPlane) {
			Vector2 centerPlanePos = new(gridSize.x / 2f, gridSize.y / 2f);
			Vector3 centerWorldPos = gridPlane.PlaneToWorldPosition(centerPlanePos, 0f);
			LevelGridBehaviour gridBehaviour = gridPool.Spawn(gridBehaviourPrefab, gridRoot);
			gridBehaviour.transform.position = centerWorldPos;
			return gridBehaviour;
		}
	}

	public class LevelGridBehaviour : MonoBehaviour {
		[SerializeField] private Transform cellRoot;

		public void Initialize() { }

		public Transform GetCellRoot() => cellRoot;
	}

	public class LevelCellBehaviour : MonoBehaviour {
		private LevelCell levelCell;

		public void Initialize(LevelCell levelCell) {
			this.levelCell = levelCell;
		}
	}
}
