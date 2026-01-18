using System;
using UnityEngine;

namespace Core.CameraSystem.PositionModifiers {
	[Serializable]
	public class RelativeSphereClamp : IPositionModifier {
		[SerializeField] private Transform target;
		[SerializeField] private float radius = 10f;

		public Vector3 Modify(Vector3 position) {
			if (target == null)
				return position;

			Vector3 center = target.position;
			Vector3 offset = position - center;

			// If camera is outside the sphere, clamp it back to the surface
			if (offset.sqrMagnitude <= radius * radius)
				return position;

			offset = offset.normalized * radius;
			return center + offset;

		}
	}
}
