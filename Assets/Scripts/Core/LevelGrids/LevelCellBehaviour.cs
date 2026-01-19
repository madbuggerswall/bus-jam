using UnityEngine;

namespace Core.LevelGrids {
	public class LevelCellBehaviour : MonoBehaviour {
		[SerializeField] private new Collider collider;

		private LevelCell levelCell;

		public void Initialize(LevelCell levelCell) {
			this.levelCell = levelCell;
		}

		public Collider GetCollider() => collider;
		public LevelCell GetCell() => levelCell;
	}
}
