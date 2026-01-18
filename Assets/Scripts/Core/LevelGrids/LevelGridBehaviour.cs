using Core.Data;
using Frolics.Grids;
using Frolics.Pooling;
using Frolics.Utilities;
using UnityEngine;

namespace Core.LevelGrids {
	public interface ILevelGridBehaviourFactory {
		public LevelGridBehaviour Create(LevelData data);
	}

	public class LevelGridBehaviourFactory : MonoBehaviour, IInitializable, ILevelGridBehaviourFactory {
		[SerializeField] private Transform gridRoot;
		[SerializeField] private Transform gridPoolRoot;
		[SerializeField] private Transform cellPoolRoot;

		[SerializeField] private LevelGridBehaviour gridBehaviourPrefab;
		[SerializeField] private LevelCellBehaviour cellBehaviourPrefab;

		private IObjectPool<LevelGridBehaviour> gridPool;
		private IObjectPool<LevelCellBehaviour> cellPool;

		void IInitializable.Initialize() {
			gridPool = new ObjectPool<LevelGridBehaviour>(gridPoolRoot);
			cellPool = new ObjectPool<LevelCellBehaviour>(cellPoolRoot);
		}

		public LevelGridBehaviour Create(LevelData data) {
			Vector2Int gridSize = data.GetGridSize();
			Vector3 pivotLocalPos = new((float) gridSize.x / 2 - 1, 0, (float) gridSize.y / 2 - 1);
			CellData[] cellDTOs = data.GetCells();

			LevelGridBehaviour gridBehaviour = gridPool.Spawn(gridBehaviourPrefab, gridRoot);
			LevelGrid levelGrid = new(gridBehaviour.transform, pivotLocalPos, gridSize, 1f, GridPlane.XZ);
			for (int i = 0; i < cellDTOs.Length; i++) {
				CellData cellDTO = cellDTOs[i];
				if(levelGrid.TryGetCell(cellDTO.GetLocalCoord(), out LevelCell cell))
					cell.SetReachable(cellDTO.GetCellType() is not CellType.Empty);
			}

			LevelCell[] cells = levelGrid.GetCells();
			for (int i = 0; i < cells.Length; i++) {
				LevelCell cell = cells[i];
				Vector3 position = levelGrid.GetWorldPosition(cell);
				cellPool.Spawn(cellBehaviourPrefab, gridBehaviour.GetCellRoot());
			}

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
