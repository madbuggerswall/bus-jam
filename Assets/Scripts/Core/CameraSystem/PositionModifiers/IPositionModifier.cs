using UnityEngine;

namespace Core.CameraSystem.PositionModifiers {
	public interface IPositionModifier {
		public Vector3 Modify(Vector3 position);
	}
}
