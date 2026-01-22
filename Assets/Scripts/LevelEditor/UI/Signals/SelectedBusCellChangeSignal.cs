using Frolics.Signals;
using LevelEditor.BusGrids;

namespace LevelEditor.UI.Signals {
	public struct SelectedBusCellChangeSignal : ISignal {
		public BusCell Cell { get; }

		public SelectedBusCellChangeSignal(BusCell cell) {
			Cell = cell;
		}
	}
}