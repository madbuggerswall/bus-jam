using Frolics.Pooling;
using Frolics.Utilities;
using UnityEngine;

namespace LevelEditor.BusGrids {
	public class BusGridBehaviourFactory : MonoBehaviour, IInitializable, IBusGridBehaviourFactory {
		[SerializeField] private Transform gridRoot;

		[SerializeField] private BusGridBehaviour gridBehaviourPrefab;

		private IObjectPool<BusGridBehaviour> gridPool;

		void IInitializable.Initialize() {
			gridPool = new ObjectPool<BusGridBehaviour>(transform);
		}

		BusGridBehaviour IBusGridBehaviourFactory.Create() {
			BusGridBehaviour gridBehaviour = gridPool.Spawn(gridBehaviourPrefab, gridRoot);
			gridBehaviour.Initialize();
			gridBehaviour.transform.localPosition = Vector3.zero;
			return gridBehaviour;
		}

		void IBusGridBehaviourFactory.Despawn(BusGridBehaviour gridBehaviour) {
			gridPool.Despawn(gridBehaviour);
		}
	}
}
