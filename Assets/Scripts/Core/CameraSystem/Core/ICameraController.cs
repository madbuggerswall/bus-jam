using Core.CameraSystem.PositionBases;
using Core.CameraSystem.PositionContributors;
using Core.CameraSystem.PositionModifiers;
using UnityEngine;

namespace Core.CameraSystem.Core {
	public interface ICameraController {
		public T GetPositionBase<T>() where T : class, IPositionBase;
		public T GetPositionContributor<T>() where T : class, IPositionContributor;
		public T GetPositionModifier<T>() where T : class, IPositionModifier;
		public void OnTargetObjectWarped(Vector3 positionDelta);
	}
}
