using System;
using UnityEngine;

namespace Core.CameraSystem.PositionBases {
	[Serializable]
	public class EmptyPositionBase : IPositionBase {
		private readonly Vector3 initialPosition;

		public EmptyPositionBase(Vector3 initialPosition) => this.initialPosition = initialPosition;
		Vector3 IPositionBase.GetPosition(float deltaTime) => initialPosition;
		void IPositionBase.OnTargetObjectWarped(Vector3 positionDelta) { }
	}
}