using System;
using Core.Data;
using UnityEngine;

namespace Core.Levels {
	[Serializable]
	public class LevelPack {
		[SerializeField] private LevelDefinition[] levelDefinitions;
		public LevelDefinition[] GetLevelDefinitions() => levelDefinitions;
	}
}