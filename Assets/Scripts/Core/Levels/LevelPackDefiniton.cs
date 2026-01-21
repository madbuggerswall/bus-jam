using System;
using Core.Data;
using UnityEngine;

namespace Core.Levels {
	[CreateAssetMenu(menuName = MenuName, fileName = FileName)]
	public class LevelPackDefinition : ScriptableObject {
		private const string MenuName = "Definition/Levels/" + FileName;
		private const string FileName = nameof(LevelPackDefinition);

		[SerializeField] private LevelPack levelPack;
		public LevelPack GetLevelPack() => levelPack;
	}

	[Serializable]
	public class LevelPack {
		[SerializeField] private LevelDefinition[] levelDefinitions;
		public LevelDefinition[] GetLevelDefinitions() => levelDefinitions;
	}
}
