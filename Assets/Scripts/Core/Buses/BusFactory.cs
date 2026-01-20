using Core.Data;
using Frolics.Contexts;
using Frolics.Pooling;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Buses {
	public class BusFactory : MonoBehaviour, IInitializable, IBusFactory {
		[SerializeField] private Transform busRoot;
		[SerializeField] private Transform spawnPoint;
		[SerializeField] private Bus busPrefab;

		private IObjectPool<Bus> pool;
		private IPassengerColorManager colorManager;


		void IInitializable.Initialize() {
			pool = new ObjectPool<Bus>(transform);
			colorManager = Context.Resolve<IPassengerColorManager>();
		}

		Bus IBusFactory.CreateBus(BusData busData) {
			ColorDefinition colorDefinition = colorManager.GetColorDefinition(busData.GetPassengerColor());
			Bus bus = pool.Spawn(busPrefab, busRoot);
			bus.Initialize(busData, colorDefinition);
			bus.transform.position = spawnPoint.position;
			return bus;
		}
	}
}
