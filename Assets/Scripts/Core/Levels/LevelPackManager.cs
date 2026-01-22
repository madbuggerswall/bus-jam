using Core.Data;
using Core.Persistence;
using Frolics.Contexts;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Levels {
	public class LevelPackManager : MonoBehaviour, IInitializable, ILevelPackManager {
		[SerializeField] private LevelPackDefinition levelPackDefinition;

		private int currentLevelIndex;

		// Services
		private IPersistenceManager persistenceManager;

		void IInitializable.Initialize() {
			persistenceManager = Context.Resolve<IPersistenceManager>();
			if (persistenceManager.TryLoad(out PlayerData playerData))
				currentLevelIndex = playerData.GetLastPlayedLevelIndex();
		}

		LevelDefinition ILevelPackManager.GetLastPlayedLevel() {
			return levelPackDefinition.GetLevelPack().GetLevelDefinitions()[currentLevelIndex];
		}

		int ILevelPackManager.GetLevelCount() {
			return levelPackDefinition.GetLevelPack().GetLevelDefinitions().Length;
		}

		public int GetCurrentLevelIndex() => currentLevelIndex;
	}
}
