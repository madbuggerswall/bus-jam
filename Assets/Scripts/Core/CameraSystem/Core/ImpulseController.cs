using Core.CameraSystem.Definitions;
using Core.CameraSystem.PositionContributors;
using Frolics.Contexts;
using Frolics.Signals;
using Frolics.Utilities;
using UnityEngine;

namespace Core.CameraSystem.Core {
	public class ImpulseController : MonoBehaviour, IInitializable {
		[SerializeField] private ImpulseShakeDefinition defaultShake;

		[SerializeField] private ImpulseShakeDefinition extractionShake;
		[SerializeField] private ImpulseShakeDefinition destroyShake;

		[SerializeField] private ImpulseShakeDefinition upgradeLandShake;
		[SerializeField] private ImpulseShakeDefinition upgradeClearShake;
		[SerializeField] private ImpulseShakeDefinition upgradePickupShake;

		// Services
		private ISignalBus signalBus;
		private ImpulseShake impulseShake;

		void IInitializable.Initialize() {
			signalBus = Context.Resolve<ISignalBus>();
			ICameraController cameraController = Context.Resolve<ICameraController>();
			impulseShake = cameraController.GetPositionContributor<ImpulseShake>();

			// signalBus.SubscribeTo<ObstacleExtractSignal>(OnObstacleExtract);
			// signalBus.SubscribeTo<UpgradeThresholdMetSignal>(OnUpgradeThresholdMet);
			// signalBus.SubscribeTo<UpgradeLandedSignal>(OnUpgradeLanded);
			// signalBus.SubscribeTo<VehicleMountSignal>(OnVehicleMount);
		}
	}
}
