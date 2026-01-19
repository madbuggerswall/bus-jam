using UnityEngine;

namespace Core.Data {
	[CreateAssetMenu(menuName = MenuName, fileName = FileName)]
	public class LevelDefinition : ScriptableObject {
		private const string MenuName = "Definition/Levels/" + FileName;
		private const string FileName = nameof(LevelDefinition);

		[SerializeField] private LevelData levelData;
		public LevelData GetLevelData() => levelData;
	}
}
