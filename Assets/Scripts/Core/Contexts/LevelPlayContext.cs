using Core.Buses;
using Core.CameraSystem.Core;
using Core.Passengers;
using Frolics.Contexts;
using UnityEngine;

namespace Core.Contexts {
	public class LevelPlayContext : SubContext<LevelPlayContext> {
		protected override void BindContext() {
			Bind<CameraController>().To<ICameraController>().To<IMainCameraProvider>();

			Bind<PassengerFactory>().To<IPassengerFactory>();
			Bind<BusFactory>().To<IBusFactory>();
		}

		protected override void OnInitialized() {
			Debug.Log($"{nameof(LevelPlayContext)} initialized.");
		}
	}
}
