using Frolics.Utilities;
using UnityEngine;

namespace Core.Mechanics {
	public class LevelTimeProvider : MonoBehaviour, IInitializable, ILevelTimeProvider {
		[SerializeField] private float levelTime = 300;

		void IInitializable.Initialize() { }

		public float GetLevelTime() => levelTime;
		public void SetLevelTime(float levelTime) => this.levelTime = levelTime;
	}
}
