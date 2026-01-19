using Core.Data;
using Core.Levels;
using Core.Passengers;
using Frolics.Contexts;
using Frolics.Utilities;

namespace Core.Buses {
	public class BusManager : IInitializable {
		private BusData[] busDTOs;
		private int currentIndex = 0;

		private Bus nextBus;
		private Bus currentBus;

		// Services
		private IBusController busController;
		private IBusFactory busFactory;
		private ILevelLoader levelLoader;


		void IInitializable.Initialize() {
			busFactory = Context.Resolve<IBusFactory>();
			levelLoader = Context.Resolve<ILevelLoader>();
			busController = Context.Resolve<IBusController>();

			busDTOs = levelLoader.GetLevelData().GetBuses();

			// Init
			BusData currentBusData = busDTOs[currentIndex++];
			BusData nextBusData = busDTOs[currentIndex++];

			currentBus = busFactory.CreateBus(currentBusData);
			busController.PlayStartToStopTween(currentBus);

			nextBus = busFactory.CreateBus(nextBusData);
			busController.PlaySpawnToStartTween(nextBus);
		}

		public void TryMountPassenger(Passenger passenger) { }

		public void OnBusFill() {
			currentBus = nextBus;
		}

		public Bus GetCurrentBus() => currentBus;
	}
}
