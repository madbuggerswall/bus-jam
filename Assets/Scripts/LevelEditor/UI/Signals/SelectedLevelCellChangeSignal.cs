using Core.LevelGrids;
using Frolics.Signals;

namespace LevelEditor.UI.Signals {
	public struct SelectedLevelCellChangeSignal : ISignal {
		public LevelCell Cell { get; }

		public SelectedLevelCellChangeSignal(LevelCell cell) {
			Cell = cell;
		}
	}
}