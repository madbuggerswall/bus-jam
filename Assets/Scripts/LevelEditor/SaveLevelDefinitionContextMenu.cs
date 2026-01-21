using Core.Levels;
using Frolics.Contexts;
using UnityEditor;

namespace LevelEditor {
	public static class SaveLevelDefinitionContextMenu {
		private const string MenuPath = "Assets/Create/Levels/LevelDefinition/";
		private const string FileName = "Save Current Level";

		[MenuItem(MenuPath + FileName)]
		private static void SaveLevelDefinition() {
			Context.Resolve<ILevelDefinitionSaver>().SaveLevelDefinition();
		}
	}

	public static class LoadLevelDefinitionContextMenu {
		private const string MenuPath = "Assets/Create/Levels/LevelDefinition/";
		private const string FileName = "Load Level";

		[MenuItem(MenuPath + FileName)]
		private static void SaveLevelDefinition() {
			Context.Resolve<IEditorLevelLoader>().LoadLevel();
		}
	}
}