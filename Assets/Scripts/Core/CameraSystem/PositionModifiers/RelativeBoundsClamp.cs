using System;
using UnityEngine;

namespace Core.CameraSystem.PositionModifiers {
	[Serializable]
	public class RelativeBoundsClamp : IPositionModifier {
		[SerializeField] private Transform target;
		[SerializeField] private Vector3 minOffset;
		[SerializeField] private Vector3 maxOffset;

		public Vector3 Modify(Vector3 position) {
			// Compute bounds relative to target
			Vector3 minBounds = target.position + minOffset;
			Vector3 maxBounds = target.position + maxOffset;

			return new Vector3(
				Mathf.Clamp(position.x, minBounds.x, maxBounds.x),
				Mathf.Clamp(position.y, minBounds.y, maxBounds.y),
				Mathf.Clamp(position.z, minBounds.z, maxBounds.z)
			);
		}
	}
}
