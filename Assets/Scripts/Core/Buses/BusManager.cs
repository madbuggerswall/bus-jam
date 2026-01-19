using System.Collections.Generic;
using Core.Data;
using Core.Passengers;
using Frolics.Contexts;
using Frolics.Utilities;

namespace Core.Buses {
	public class BusManager : IInitializable {
		private List<BusData> busDTOs = new();
		private int currentIndex = 0;

		private Bus nextBus;
		private Bus currentBus;

		// Services
		private IBusController busController;
		private IBusFactory busFactory;


		void IInitializable.Initialize() {
			busFactory = Context.Resolve<IBusFactory>();

			BusData busData = busDTOs[currentIndex];
			currentIndex++;
			
			Bus bus = busFactory.CreateBus(busData);
			busController.PlaySpawnToStartTween(bus);
		}

		public void TryMountPassenger(Passenger passenger) { }

		public void OnBusFill() {
			currentBus = nextBus;
		}
	}
}
