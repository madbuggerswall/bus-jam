using Core.Data;
using Core.Levels;
using Core.Passengers;
using Frolics.Contexts;
using Frolics.Utilities;

namespace Core.Buses {
	public interface IBusManager {
		public Bus GetCurrentBus();
		public void OnBusFill();
	}

	public class BusManager : IInitializable, IBusManager {
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
		
		void IBusManager.OnBusFill() {
			currentBus = nextBus;
			busController.PlayStartToStopTween(currentBus);

			BusData nextBusData = busDTOs[currentIndex++];
			nextBus = busFactory.CreateBus(nextBusData);
			
			// TODO Wait for last passenger
			busController.PlaySpawnToStartTween(nextBus);
		}

		private void GetNextBus() {
			BusData nextBusData = busDTOs[currentIndex++];
			
		}

		Bus IBusManager.GetCurrentBus() => currentBus;
	}
}
