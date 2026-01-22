using Core.Buses;
using Core.CameraSystem.Core;
using Core.GridElements;
using Core.Input;
using Core.LevelGrids;
using Core.Levels;
using Core.Mechanics;
using Core.Passengers;
using Core.PathFinding;
using Core.Persistence;
using Core.Waiting.Grids;
using Frolics.Contexts;
using UnityEngine;

namespace Core.Contexts {
	public class LevelPlayContext : SubContext<LevelPlayContext> {
		protected override void BindContext() {
			// Bind<SignalBus>().To<ISignalBus>();
			Bind<CameraController>().To<ICameraController>().To<IMainCameraProvider>();

			// Factories
			Bind<LevelGridBehaviourFactory>().To<ILevelGridBehaviourFactory>();
			Bind<LevelCellBehaviourFactory>().To<ILevelCellBehaviourFactory>();
			Bind<WaitingGridBehaviourFactory>().To<IWaitingGridBehaviourFactory>();
			Bind<WaitingCellBehaviourFactory>().To<IWaitingCellBehaviourFactory>();
			Bind<GridElementFactory>().To<IGridElementFactory>();
			Bind<BusFactory>().To<IBusFactory>();

			// Level Initialization
			Bind<PersistenceManager>().To<IPersistenceManager>();
			Bind<LevelPackManager>().To<ILevelPackManager>();
			Bind<LevelLoader>().To<ILevelLoader>();
			Bind<PassengerSpawner>().To<IPassengerSpawner>();
			Bind<LevelGridInitializer>().To<ILevelGridBehaviourProvider>().To<ILevelGridProvider>();
			Bind<WaitingGridInitializer>().To<IWaitingGridBehaviourProvider>().To<IWaitingGridProvider>();
			Bind<PathFinder>().To<IPathFinder>();

			Bind<LevelAreaManager>().To<ILevelAreaManager>();
			Bind<WaitingAreaManager>().To<IWaitingAreaManager>();

			// Mechanics (View)
			Bind<PassengerController>().To<IPassengerController>();
			Bind<BusController>().To<IBusController>();
			Bind<BoardingSequenceController>();
			
			// Mechanics
			Bind<BusManager>().To<IBusManager>();
			Bind<RuleService>().To<IRuleService>();
			Bind<TimerManager>().To<ITimerManager>();

			// Input
			Bind<CellBehaviourMapper>().To<ICellBehaviourMapper>();
			Bind<PassengerClickHandler>();

			Bind<LevelStateManager>().To<ILevelStateManager>();
		}

		protected override void OnInitialized() {
			Debug.Log($"{nameof(LevelPlayContext)} initialized.");
		}
	}
}
