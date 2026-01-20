using Frolics.Utilities;
using UnityEngine;

namespace Core.Data {
	public class LevelTimeProvider : MonoBehaviour, IInitializable, ILevelTimeProvider {
		[SerializeField] private float levelTime;

		void IInitializable.Initialize() { }

		public float GetLevelTime() {
			return levelTime;
		}
	}
}
