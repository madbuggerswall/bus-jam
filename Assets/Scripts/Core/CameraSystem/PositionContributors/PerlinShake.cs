using System;
using UnityEngine;

namespace Core.CameraSystem.PositionContributors {
	[Serializable]
	public class PerlinShake : IPositionContributor {
		[SerializeField] private Vector3 pivotOffset = Vector3.zero;
		[SerializeField] private Vector3 amplitudeGain = new(1f, 1f, 1f);
		[SerializeField] private Vector3 frequencyGain = new(.2f, .2f, .2f);
		[SerializeField] private int noiseSeed = 0;

		// Fields
		private float time;

		public Vector3 GetOffset(float deltaTime) {
			time += deltaTime;
			float nx = Mathf.PerlinNoise(noiseSeed, time * frequencyGain.x);
			float ny = Mathf.PerlinNoise(noiseSeed + 1, time * frequencyGain.y);
			float nz = Mathf.PerlinNoise(noiseSeed + 2, time * frequencyGain.z);

			// Remap [0,1] → [-1,1]
			nx = (nx * 2f - 1f);
			ny = (ny * 2f - 1f);
			nz = (nz * 2f - 1f);

			// Scale by amplitude
			Vector3 noiseOffset = new(nx * amplitudeGain.x, ny * amplitudeGain.y, nz * amplitudeGain.z);

			return pivotOffset + noiseOffset;
		}
	}
}