using UnityEngine;

namespace Core.Waiting.Grids {
	public class WaitingCellBehaviour : MonoBehaviour {
		private WaitingCell waitingCell;

		public void Initialize(WaitingCell levelCell) {
			this.waitingCell = levelCell;
		}

		public WaitingCell GetCell() => waitingCell;
	}
}