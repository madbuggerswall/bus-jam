using UnityEngine;

namespace Core.LevelGrids {
	public class LevelCellBehaviour : MonoBehaviour {
		[SerializeField] private new Collider collider;
		[SerializeField] private Transform meshTransform;

		private LevelCell levelCell;

		public void Initialize(LevelCell levelCell) {
			this.levelCell = levelCell;
			meshTransform.gameObject.SetActive(levelCell.IsReachable());
		}

		public Collider GetCollider() => collider;
		public LevelCell GetCell() => levelCell;
	}
}
