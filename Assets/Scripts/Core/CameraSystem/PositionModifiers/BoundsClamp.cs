using System;
using UnityEngine;

namespace Core.CameraSystem.PositionModifiers {
	[Serializable]
	public class BoundsClamp : IPositionModifier {
		[SerializeField] private Vector3 minBounds;
		[SerializeField] private Vector3 maxBounds;

		public Vector3 Modify(Vector3 position) {
			return new Vector3(
				Mathf.Clamp(position.x, minBounds.x, maxBounds.x),
				Mathf.Clamp(position.y, minBounds.y, maxBounds.y),
				Mathf.Clamp(position.z, minBounds.z, maxBounds.z)
			);
		}
	}
}
