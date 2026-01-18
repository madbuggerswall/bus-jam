using System;
using Frolics.Utilities.Extensions;
using UnityEngine;

namespace Core.CameraSystem.PositionModifiers {
	[Serializable]
	public class SmoothAxisClamp : IPositionModifier {
		public enum Axis { X, Y, Z }

		[SerializeField] private Axis axis = Axis.X;
		[SerializeField] private float softMinValue = 15f;
		[SerializeField] private float softMaxValue = 19f;
		[SerializeField] private float hardMinValue = 14f;
		[SerializeField] private float hardMaxValue = 20f;
		[SerializeField] private float smoothness = .4f; // higher = softer clamp

		public Vector3 Modify(Vector3 position) {
			return axis switch {
				Axis.X => position.WithX(SmoothClamp(position.x)),
				Axis.Y => position.WithY(SmoothClamp(position.y)),
				Axis.Z => position.WithZ(SmoothClamp(position.z)),
				_ => throw new ArgumentOutOfRangeException()
			};
		}

		private float SmoothClamp(float value) {
			if (value < softMinValue) {
				// push back smoothly toward min
				float delta = softMinValue - value;
				float smoothedClamp = softMinValue - delta / (1f + delta * smoothness);
				return Mathf.Clamp(smoothedClamp, hardMinValue, hardMaxValue);
			}

			if (value > softMaxValue) {
				// push back smoothly toward max
				float delta = value - softMaxValue;
				float smoothedClamp = softMaxValue + delta / (1f + delta * smoothness);
				return Mathf.Clamp(smoothedClamp, hardMinValue, hardMaxValue);
			}

			return Mathf.Clamp(value, hardMinValue, hardMaxValue);
		}
	}
}
