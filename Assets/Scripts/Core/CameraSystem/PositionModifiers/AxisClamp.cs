using System;
using Frolics.Utilities.Extensions;
using UnityEngine;

namespace Core.CameraSystem.PositionModifiers {
	public class AxisClamp : IPositionModifier {
		private enum Axis { X, Y, Z }

		[SerializeField] private Axis axis;
		[SerializeField] private float minValue;
		[SerializeField] private float maxValue;


		public Vector3 Modify(Vector3 position) {
			return axis switch {
				Axis.X => position.WithX(Mathf.Clamp(position.x, minValue, maxValue)),
				Axis.Y => position.WithY(Mathf.Clamp(position.y, minValue, maxValue)),
				Axis.Z => position.WithZ(Mathf.Clamp(position.z, minValue, maxValue)),
				_ => throw new ArgumentOutOfRangeException()
			};
		}
	}
}
