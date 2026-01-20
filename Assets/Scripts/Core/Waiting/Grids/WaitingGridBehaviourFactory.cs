using Frolics.Pooling;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Waiting.Grids {
	public class WaitingGridBehaviourFactory : MonoBehaviour, IInitializable, IWaitingGridBehaviourFactory {
		[SerializeField] private Transform gridRoot;

		[SerializeField] private WaitingGridBehaviour gridBehaviourPrefab;

		private IObjectPool<WaitingGridBehaviour> gridPool;

		void IInitializable.Initialize() {
			gridPool = new ObjectPool<WaitingGridBehaviour>(transform);
		}

		WaitingGridBehaviour IWaitingGridBehaviourFactory.Create() {
			WaitingGridBehaviour gridBehaviour = gridPool.Spawn(gridBehaviourPrefab, gridRoot);
			gridBehaviour.Initialize();
			gridBehaviour.transform.localPosition = Vector3.zero;
			return gridBehaviour;
		}
		
		void IWaitingGridBehaviourFactory.Despawn(WaitingGridBehaviour gridBehaviour) {
			gridPool.Despawn(gridBehaviour);
		}
	}
}