using Core.Data;
using Frolics.Signals;

namespace LevelEditor.SaveSystem {
	public struct EditorLevelLoadedSignal : ISignal {
		public LevelDTO LevelDTO { get; }

		public EditorLevelLoadedSignal(LevelDTO levelDTO) {
			LevelDTO = levelDTO;
		}
	}
}