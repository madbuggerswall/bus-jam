using System;
using Core.CameraSystem.Definitions;
using Core.CameraSystem.DTOs;
using Frolics.Tweens.Easing;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.CameraSystem.PositionContributors {
	[Serializable]
	public class ImpulseShake : IPositionContributor {
		// TODO Shakes can be layered or combined
		// TODO Rename this and other contributors to ImpulseShakeContributor and vice versa.
		// IDEA ImpulseShakeFactory
		// IDEA Have a magnitude multiplier
		[SerializeField] private ImpulseShakeDefinition defaultShakeDefinition;

		// Fields
		private ImpulseShakeDTO impulseShakeDTO;
		private float elapsedTime;
		private float phaseX;
		private float phaseY;
		private float phaseZ;

		public void TriggerShake(ImpulseShakeDTO impulseShakeDTO) {
			this.impulseShakeDTO = impulseShakeDTO;
			BeginShake();
		}

		public void TriggerShake(Vector3 magnitude, Vector3 frequency, float duration, Ease.Type easeType) {
			impulseShakeDTO = new ImpulseShakeDTO(magnitude, frequency, duration, easeType);
			BeginShake();
		}

		public void TriggerShake(ImpulseShakeDefinition shakeDefinition) {
			impulseShakeDTO = new ImpulseShakeDTO(shakeDefinition);
			BeginShake();
		}

		public void TriggerShake() => TriggerShake(defaultShakeDefinition);

		// Randomize phase offsets so each axis wiggles differently
		private void BeginShake() {
			elapsedTime = 0f;
			phaseX = Random.Range(0f, Mathf.PI * 2f);
			phaseY = Random.Range(0f, Mathf.PI * 2f);
			phaseZ = Random.Range(0f, Mathf.PI * 2f);
		}

		Vector3 IPositionContributor.GetOffset(float deltaTime) {
			Vector3 frequency = impulseShakeDTO.Frequency;
			Vector3 magnitude = impulseShakeDTO.Magnitude;
			float duration = impulseShakeDTO.Duration;
			Ease.Type easeType = impulseShakeDTO.EaseType;

			if (elapsedTime >= duration)
				return Vector3.zero;

			elapsedTime += deltaTime;

			// Dampen amplitude over time
			float damper = Ease.Get(easeType).Invoke(1 - elapsedTime / duration);
			float x = Mathf.Sin(elapsedTime * frequency.x + phaseX) * magnitude.x * damper;
			float y = Mathf.Sin(elapsedTime * frequency.y + phaseY) * magnitude.y * damper;
			float z = Mathf.Sin(elapsedTime * frequency.z + phaseZ) * magnitude.z * damper;

			return new Vector3(x, y, z);
		}
	}
}
