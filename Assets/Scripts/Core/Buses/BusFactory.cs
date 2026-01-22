using Core.Data;
using Frolics.Pooling;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Buses {
	public class BusFactory : MonoBehaviour, IInitializable, IBusFactory {
		[SerializeField] private Transform busRoot;
		[SerializeField] private Transform spawnPoint;
		[SerializeField] private Bus busPrefab;

		private IObjectPool<Bus> pool;


		void IInitializable.Initialize() {
			pool = new ObjectPool<Bus>(transform);
		}

		Bus IBusFactory.CreateBus(BusDTO busDTO) {
			Bus bus = pool.Spawn(busPrefab, busRoot);
			bus.Initialize(busDTO);
			bus.transform.position = spawnPoint.position;
			return bus;
		}

		void IBusFactory.Despawn(Bus bus) {
			pool.Despawn(bus);
		}
	}
}
