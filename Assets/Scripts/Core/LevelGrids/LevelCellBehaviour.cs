using UnityEngine;

namespace Core.LevelGrids {
	public class LevelCellBehaviour : MonoBehaviour {
		private LevelCell levelCell;

		public void Initialize(LevelCell levelCell) {
			this.levelCell = levelCell;
		}
	}
}