using Core.Buses;
using Core.CameraSystem.Core;
using Core.Input;
using Core.LevelGrids;
using Core.Levels;
using Core.Passengers;
using Frolics.Contexts;
using UnityEngine;

namespace Core.Contexts {
	public class LevelPlayContext : SubContext<LevelPlayContext> {
		protected override void BindContext() {
			Bind<CameraController>().To<ICameraController>().To<IMainCameraProvider>();

			Bind<LevelGridBehaviourFactory>().To<ILevelGridBehaviourFactory>();
			Bind<LevelCellBehaviourFactory>().To<ILevelCellBehaviourFactory>();
			Bind<GridElementFactory>().To<IGridElementFactory>();
			Bind<BusFactory>().To<IBusFactory>();
			
			Bind<PassengerSpawner>().To<IPassengerSpawner>();
			Bind<LevelLoader>().To<ILevelLoader>();

			Bind<PassengerClickHandler>();
		}

		protected override void OnInitialized() {
			Debug.Log($"{nameof(LevelPlayContext)} initialized.");
		}
	}
}
