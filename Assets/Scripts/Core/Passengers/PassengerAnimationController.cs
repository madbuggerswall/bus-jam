using System;
using UnityEngine;

namespace Core.Scavenger {
	public class PassengerAnimationController : MonoBehaviour {
		[SerializeField] private Animator animator;
		[SerializeField] private float rotateSpeed = 10f;

		private static readonly int WalkRunBlendID = Animator.StringToHash("WalkRunBlend");

		private Vector3 lastPosition;
		private Vector3 velocity;
		private float speed;

		private void Start() { }

		private void Update() {
			velocity = CalculateVelocity();
			speed = velocity.magnitude;

			bool hasVelocity = speed > 0.1f;
			transform.rotation = CalculateRotation(velocity, hasVelocity);
			animator.SetFloat(WalkRunBlendID, speed, .04f, Time.deltaTime);
		}

		private Vector3 CalculateVelocity() {
			Vector3 displacement = transform.position - lastPosition;
			lastPosition = transform.position;
			return displacement / Time.deltaTime;
		}

		private Quaternion CalculateRotation(Vector3 directionXZ, bool hasInput) {
			if (!hasInput)
				return transform.rotation;

			float targetAngle = Mathf.Atan2(directionXZ.x, directionXZ.z) * Mathf.Rad2Deg;
			float interpolant = Time.deltaTime * rotateSpeed;
			Quaternion targetQuaternion = Quaternion.Euler(0, targetAngle, 0);
			return Quaternion.Slerp(transform.rotation, targetQuaternion, interpolant);
		}

		public Animator GetAnimator() => animator;
	}
}
