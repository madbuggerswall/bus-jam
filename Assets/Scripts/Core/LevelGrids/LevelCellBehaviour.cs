using UnityEngine;

namespace Core.LevelGrids {
	public class LevelCellBehaviour : MonoBehaviour {
		[SerializeField] private Collider emptyCellCollider;
		[SerializeField] private Collider occupiedCellCollider;
		[SerializeField] private Transform meshTransform;

		private LevelCell levelCell;

		public void Initialize(LevelCell levelCell) {
			this.levelCell = levelCell;
			meshTransform.gameObject.SetActive(levelCell.IsReachable());
			emptyCellCollider.enabled = !levelCell.HasElement();
			occupiedCellCollider.enabled = levelCell.HasElement();
		}

		public Collider GetOccupiedCellCollider() => occupiedCellCollider;
		public Collider GetEmptyCellCollider() => emptyCellCollider;
		public LevelCell GetCell() => levelCell;
	}
}
