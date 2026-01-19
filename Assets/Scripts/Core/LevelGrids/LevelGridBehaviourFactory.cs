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

		LevelGridBehaviour ILevelGridBehaviourFactory.Create() {
			LevelGridBehaviour gridBehaviour = gridPool.Spawn(gridBehaviourPrefab, gridRoot);
			gridBehaviour.transform.localPosition = Vector3.zero;
			return gridBehaviour;
		}
	}
}
