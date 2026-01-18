using Core.CameraSystem.Definitions;
using Frolics.Tweens.Easing;
using UnityEngine;

namespace Core.CameraSystem.DTOs {
	public struct ImpulseShakeDTO {
		public Vector3 Frequency { get; }
		public Vector3 Magnitude { get; }
		public float Duration { get; }
		public Ease.Type EaseType { get; }

		public ImpulseShakeDTO(ImpulseShakeDefinition shakeDefinition) {
			Duration = shakeDefinition.GetDuration();
			Frequency = shakeDefinition.GetFrequency();
			Magnitude = shakeDefinition.GetMagnitude();
			EaseType = shakeDefinition.GetEaseType();
		}

		public ImpulseShakeDTO(Vector3 magnitude, Vector3 frequency, float duration, Ease.Type easeType) {
			Duration = duration;
			Frequency = frequency;
			Magnitude = magnitude;
			EaseType = easeType;
		}
	}
}
