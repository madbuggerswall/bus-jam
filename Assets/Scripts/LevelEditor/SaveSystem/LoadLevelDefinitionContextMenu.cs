using Frolics.Contexts;
using UnityEditor;

namespace LevelEditor.SaveSystem {
	public static class LoadLevelDefinitionContextMenu {
		private const string MenuPath = "Assets/Create/Levels/LevelDefinition/";
		private const string FileName = "Load Level";

		[MenuItem(MenuPath + FileName)]
		private static void SaveLevelDefinition() {
			Context.Resolve<IEditorLevelLoader>().LoadLevel();
		}
	}
}