using UnityEngine;

namespace Core.CameraSystem.PositionBases {
	public interface IPositionBase {
		public Vector3 GetPosition(float deltaTime);
		public void OnTargetObjectWarped(Vector3 positionDelta);
	}
}