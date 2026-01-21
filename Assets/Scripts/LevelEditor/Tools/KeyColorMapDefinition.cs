using UnityEngine;

namespace LevelEditor.Tools {
	[CreateAssetMenu(menuName = MenuName, fileName = FileName)]
	public class KeyColorMapDefinition : ScriptableObject {
		private const string FileName = nameof(KeyColorMapDefinition);
		private const string MenuName = "Definition/Editor/" + FileName;

		[SerializeField] private KeyColorMapping[] keyColorMappings;
		public KeyColorMapping[] GetKeyColorMappings() => keyColorMappings;
	}
}