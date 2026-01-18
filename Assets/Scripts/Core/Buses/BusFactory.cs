using Frolics.Pooling;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Buses {
	public interface IBusFactory {
		public Bus CreateBus();
	}

	public class BusFactory : MonoBehaviour, IInitializable, IBusFactory {
		[SerializeField] private Transform busRoot;
		[SerializeField] private Bus busPrefab;

		private IObjectPool<Bus> pool;

		void IInitializable.Initialize() {
			pool = new ObjectPool<Bus>(transform);
		}

		// TODO
		public Bus CreateBus() {
			return pool.Spawn(busPrefab, busRoot);
		}
	}
}
