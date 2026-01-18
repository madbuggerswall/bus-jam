using Frolics.Grids;
using Frolics.Pooling;
using Frolics.Utilities;
using UnityEngine;

namespace Core.LevelGrids {
	public class LevelGridBehaviourFactory : MonoBehaviour, IInitializable, ILevelGridBehaviourFactory {
		[SerializeField] private Transform gridRoot;

		[SerializeField] private LevelGridBehaviour gridBehaviourPrefab;

		private IObjectPool<LevelGridBehaviour> gridPool;

		void IInitializable.Initialize() {
			gridPool = new ObjectPool<LevelGridBehaviour>(transform);
		}

		LevelGridBehaviour ILevelGridBehaviourFactory.Create(Vector2Int gridSize, GridPlane gridPlane) {
			Vector2 centerPlanePos = new(gridSize.x / 2f, gridSize.y / 2f);
			Vector3 centerWorldPos = gridPlane.PlaneToWorldPosition(centerPlanePos, 0f);
			LevelGridBehaviour gridBehaviour = gridPool.Spawn(gridBehaviourPrefab, gridRoot);
			gridBehaviour.transform.position = centerWorldPos;
			return gridBehaviour;
		}
	}
}