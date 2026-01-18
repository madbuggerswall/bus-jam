using Frolics.Tweens.Easing;
using UnityEngine;

namespace Core.CameraSystem.Definitions {
	[CreateAssetMenu(menuName = MenuName, fileName = FileName)]
	public class ImpulseShakeDefinition : ScriptableObject {
		private const string MenuName = "Definition/Camera/" + FileName;
		private const string FileName = nameof(ImpulseShakeDefinition);

		[SerializeField] private Vector3 magnitude;
		[SerializeField] private Vector3 frequency;
		[SerializeField] private float duration;
		[SerializeField] private Ease.Type easeType = Ease.Type.InOutQuad;

		public Vector3 GetMagnitude() => magnitude;
		public Vector3 GetFrequency() => frequency;
		public float GetDuration() => duration;
		public Ease.Type GetEaseType() => easeType;
	}
}
