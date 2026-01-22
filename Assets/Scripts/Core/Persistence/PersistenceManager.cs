using System;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Persistence {
	public interface IPersistenceManager {
		public bool TryLoad(out PlayerData playerData);
		public void Save(PlayerData playerData);
	}

	public class PersistenceManager : MonoBehaviour, IInitializable, IPersistenceManager {
		private const string PlayerDataPath = "/PlayerData.dat";

		void IInitializable.Initialize() { }


		bool IPersistenceManager.TryLoad(out PlayerData playerData) {
			playerData = default;

			if (!SaveManager.Exists(GetPlayerDataPath()))
				return false;

			playerData = SaveManager.Load<PlayerData>(GetPlayerDataPath());
			return true;
		}

		void IPersistenceManager.Save(PlayerData playerData) {
			SaveManager.Save(playerData, GetPlayerDataPath());
		}

		[ContextMenu("Delete Data")]
		private void DeleteData() {
			if (!SaveManager.Exists(GetPlayerDataPath()))
				return;

			SaveManager.Delete(GetPlayerDataPath());
		}

		private static string GetPlayerDataPath() {
			return Application.persistentDataPath + PlayerDataPath;
		}
	}

	[Serializable]
	public struct PlayerData {
		[SerializeField] private int lastPlayedLevelIndex;

		public PlayerData(int lastPlayedLevelIndex) => this.lastPlayedLevelIndex = lastPlayedLevelIndex;
		public int GetLastPlayedLevelIndex() => lastPlayedLevelIndex;
		public void SetLastPlayedLevelIndex(int levelIndex) => lastPlayedLevelIndex = levelIndex;
	}
}
