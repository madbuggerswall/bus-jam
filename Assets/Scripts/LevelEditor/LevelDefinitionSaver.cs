using Core.Data;
using Frolics.Contexts;
using Frolics.Utilities;
using UnityEditor;
using UnityEngine;

namespace LevelEditor {
	public class LevelDefinitionSaver : IInitializable, ILevelDefinitionSaver {
		// Services
		private ILevelDataSaver levelDataSaver;

		void IInitializable.Initialize() {
			levelDataSaver = Context.Resolve<ILevelDataSaver>();
		}

		void ILevelDefinitionSaver.SaveLevelDefinition() {
			string path = PromptUserForSaveLocation();
			if (string.IsNullOrWhiteSpace(path)) {
				Debug.LogWarning($"Invalid file location: {path}");
				return;
			}

			LevelDefinition levelDefinition = ScriptableObject.CreateInstance<LevelDefinition>();
			LevelDTO levelDTO = levelDataSaver.SaveLevelData();
			levelDefinition.SetLevelDTO(levelDTO);

			//Save asset
			AssetDatabase.CreateAsset(levelDefinition, path);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			// Ping the new asset
			EditorGUIUtility.PingObject(levelDefinition);
		}

		private static string PromptUserForSaveLocation() {
			// Prompt user for save location
			const string title = "Create Level Definition";
			const string defaultName = "LevelDefinition";
			const string extension = "asset";
			const string message = "Choose a location to save the new LevelDefinition asset";
			return EditorUtility.SaveFilePanelInProject(title, defaultName, extension, message);
		}
	}
}
