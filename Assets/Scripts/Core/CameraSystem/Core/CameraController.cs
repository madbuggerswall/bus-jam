using System.Collections.Generic;
using Core.CameraSystem.PositionBases;
using Core.CameraSystem.PositionContributors;
using Core.CameraSystem.PositionModifiers;
using Frolics.Utilities;
using UnityEngine;

namespace Core.CameraSystem.Core {
	public class CameraController : MonoBehaviour, IInitializable, ICameraController, IMainCameraProvider {
		[SerializeField] private new Camera camera;
		[Header("Base Module")]
		[SerializeReference] private IPositionBase positionBase;
		[SerializeReference] private IPositionContributor[] positionContributors;
		[SerializeReference] private IPositionModifier[] positionModifiers;

		private Vector3 warpPositionDelta;

		private readonly Dictionary<System.Type, IPositionContributor> contributors = new();
		private readonly Dictionary<System.Type, IPositionModifier> modifiers = new();

		void IInitializable.Initialize() {
			positionBase ??= new EmptyPositionBase(camera.transform.position);

			for (int i = 0; i < positionContributors.Length; i++)
				contributors.Add(positionContributors[i].GetType(), positionContributors[i]);

			for (int i = 0; i < positionModifiers.Length; i++)
				modifiers.Add(positionModifiers[i].GetType(), positionModifiers[i]);
		}

		private void LateUpdate() {
			Vector3 cameraPosition = positionBase.GetPosition(Time.deltaTime);

			for (int i = 0; i < positionContributors.Length; i++)
				cameraPosition += positionContributors[i].GetOffset(Time.deltaTime);

			for (int i = 0; i < positionModifiers.Length; i++)
				cameraPosition = positionModifiers[i].Modify(cameraPosition);

			camera.transform.position = cameraPosition;
		}

		T ICameraController.GetPositionBase<T>() => positionBase as T;
		T ICameraController.GetPositionContributor<T>() => contributors.GetValueOrDefault(typeof(T)) as T;
		T ICameraController.GetPositionModifier<T>() => modifiers.GetValueOrDefault(typeof(T)) as T;

		void ICameraController.OnTargetObjectWarped(Vector3 positionDelta) {
			positionBase.OnTargetObjectWarped(positionDelta);
		}

		Camera IMainCameraProvider.GetMainCamera() => camera;
	}
}
