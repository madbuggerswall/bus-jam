using Core.Data;
using Frolics.Utilities;
using UnityEditor;
using UnityEngine;

namespace LevelEditor {
	public class LevelDefinitionLoader : IInitializable, ILevelDefinitionLoader {
		void IInitializable.Initialize() { }

		LevelDefinition ILevelDefinitionLoader.LoadLevelDefinition() {
			string path = PromptUserForOpenLocation();
			if (string.IsNullOrEmpty(path)) {
				Debug.LogWarning("No file selected.");
				return null;
			}

			// Convert absolute path to project relative path: "Assets/..."
			if (!path.StartsWith(Application.dataPath)) {
				Debug.LogError("Selected file is not inside the project Assets folder.");
				return null;
			}

			// Load the asset
			string projectPath = "Assets" + path.Substring(Application.dataPath.Length);
			LevelDefinition levelDefinition = AssetDatabase.LoadAssetAtPath<LevelDefinition>(projectPath);
			if (levelDefinition != null)
				return levelDefinition;

			Debug.LogError($"Failed to load LevelDefinition at {projectPath}");
			return null;
		}


		private static string PromptUserForOpenLocation() {
			const string title = "Load Level Definition";
			string directory = Application.dataPath;
			string[] filters = { "LevelDefinition", "asset" };
			return EditorUtility.OpenFilePanelWithFilters(title, directory, filters);
		}
	}
}
