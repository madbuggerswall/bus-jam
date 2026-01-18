using UnityEngine;

namespace Core.CameraSystem.PositionContributors {
	public interface IPositionContributor {
		public Vector3 GetOffset(float deltaTime);
	}
}