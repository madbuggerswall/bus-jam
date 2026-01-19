using Core.Buses;
using Core.CameraSystem.Core;
using Core.Data;
using Core.Input;
using Core.LevelGrids;
using Core.Levels;
using Core.Passengers;
using Core.PathFinding;
using Frolics.Contexts;
using UnityEngine;

namespace Core.Contexts {
	public class LevelPlayContext : SubContext<LevelPlayContext> {
		protected override void BindContext() {
			Bind<CameraController>().To<ICameraController>().To<IMainCameraProvider>();

			Bind<PassengerColorManager>().To<IPassengerColorManager>();
			Bind<LevelGridBehaviourFactory>().To<ILevelGridBehaviourFactory>();
			Bind<LevelCellBehaviourFactory>().To<ILevelCellBehaviourFactory>();
			Bind<GridElementFactory>().To<IGridElementFactory>();
			Bind<BusFactory>().To<IBusFactory>();

			Bind<PassengerSpawner>().To<IPassengerSpawner>();
			Bind<LevelLoader>().To<ILevelLoader>().To<IGridBehaviourProvider>().To<IGridProvider>();
			Bind<PathFinder>().To<IPathFinder>();

			Bind<BusController>().To<IBusController>();
			Bind<BusManager>();

			Bind<CellBehaviourMapper>().To<ICellBehaviourMapper>();
			Bind<PassengerClickHandler>();
		}

		protected override void OnInitialized() {
			Debug.Log($"{nameof(LevelPlayContext)} initialized.");
		}
	}
}
