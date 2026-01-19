using Core.Buses;
using Core.CameraSystem.Core;
using Core.Data;
using Core.Input;
using Core.LevelGrids;
using Core.Levels;
using Core.Passengers;
using Core.PathFinding;
using Core.Waiting;
using Core.Waiting.Grids;
using Frolics.Contexts;
using UnityEngine;

namespace Core.Contexts {
	public class LevelPlayContext : SubContext<LevelPlayContext> {
		protected override void BindContext() {
			Bind<CameraController>().To<ICameraController>().To<IMainCameraProvider>();

			Bind<PassengerColorManager>().To<IPassengerColorManager>();

			// Factories
			Bind<LevelGridBehaviourFactory>().To<ILevelGridBehaviourFactory>();
			Bind<LevelCellBehaviourFactory>().To<ILevelCellBehaviourFactory>();
			Bind<WaitingGridBehaviourFactory>().To<IWaitingGridBehaviourFactory>();
			Bind<WaitingCellBehaviourFactory>().To<IWaitingCellBehaviourFactory>();
			Bind<GridElementFactory>().To<IGridElementFactory>();
			Bind<BusFactory>().To<IBusFactory>();
			
			// Level Initialization
			Bind<LevelLoader>().To<ILevelLoader>();
			Bind<LevelGridInitializer>().To<ILevelGridBehaviourProvider>().To<ILevelGridProvider>();
			Bind<WaitingGridInitializer>().To<IWaitingGridBehaviourProvider>().To<IWaitingGridProvider>();
			Bind<PassengerSpawner>().To<IPassengerSpawner>();

			Bind<PathFinder>().To<IPathFinder>();
			Bind<PassengerController>().To<IPassengerController>();
			Bind<BusController>().To<IBusController>();
			Bind<BusManager>().To<IBusManager>();
			// Bind<WaitingAreaManager>().To<IWaitingAreaManager>();
			Bind<RuleManager>();
			
			Bind<CellBehaviourMapper>().To<ICellBehaviourMapper>();
			Bind<PassengerClickHandler>();
		}

		protected override void OnInitialized() {
			Debug.Log($"{nameof(LevelPlayContext)} initialized.");
		}
	}
}
