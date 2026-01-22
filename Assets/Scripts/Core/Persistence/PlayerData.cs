using System;
using UnityEngine;

namespace Core.Persistence {
	[Serializable]
	public struct PlayerData {
		[SerializeField] private int lastPlayedLevelIndex;

		public PlayerData(int lastPlayedLevelIndex) => this.lastPlayedLevelIndex = lastPlayedLevelIndex;
		public int GetLastPlayedLevelIndex() => lastPlayedLevelIndex;
		public void SetLastPlayedLevelIndex(int levelIndex) => lastPlayedLevelIndex = levelIndex;
	}
}