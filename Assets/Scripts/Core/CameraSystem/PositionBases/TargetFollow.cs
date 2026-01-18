using System;
using UnityEngine;

namespace Core.CameraSystem.PositionBases {
	[Serializable]
	public class TargetFollow : IPositionBase {
		[SerializeField] private Transform target;
		[SerializeField] private Vector3 followOffset = new Vector3(16f, 32f, -32f);
		[SerializeField] private Vector3 positionDamping = new Vector3(2f, 2f, 2f);

		private Vector3 followBase;

		Vector3 IPositionBase.GetPosition(float deltaTime) {
			Vector3 targetPos = target.position + followOffset;

			followBase = new Vector3(
				Mathf.Lerp(followBase.x, targetPos.x, deltaTime * positionDamping.x),
				Mathf.Lerp(followBase.y, targetPos.y, deltaTime * positionDamping.y),
				Mathf.Lerp(followBase.z, targetPos.z, deltaTime * positionDamping.z)
			);

			return followBase;
		}

		void IPositionBase.OnTargetObjectWarped(Vector3 positionDelta) {
			followBase += positionDelta;
		}
	}
}
