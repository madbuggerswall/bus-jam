using UnityEngine;

namespace LevelEditor {
	[CreateAssetMenu(menuName = MenuName, fileName = FileName)]
	public class KeyPrefabMapDefinition : ScriptableObject {
		private const string FileName = nameof(KeyPrefabMapDefinition);
		private const string MenuName = "Definition/Editor/" + FileName;

		[SerializeField] private KeyPrefabMapping[] keyPrefabMappings;
		public KeyPrefabMapping[] GetKeyPrefabMappings() => keyPrefabMappings;
	}
}