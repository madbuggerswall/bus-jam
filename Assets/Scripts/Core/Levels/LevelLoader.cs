using Core.Data;
using Frolics.Contexts;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Levels {
	public class LevelLoader : MonoBehaviour, IInitializable, ILevelLoader {
		[SerializeField] private LevelDefinition defaultLevelDefinition;

		// Services
		private ILevelPackManager levelPackManager;

		void IInitializable.Initialize() {
			levelPackManager = Context.Resolve<ILevelPackManager>();

			if (defaultLevelDefinition == null) {
				levelPackManager.GetLastPlayedLevel();
			}
		}

		LevelDTO ILevelLoader.GetLevelData() {
			return defaultLevelDefinition != null
				? defaultLevelDefinition.GetLevelDTO()
				: levelPackManager.GetLastPlayedLevel().GetLevelDTO();
		}
	}
}
