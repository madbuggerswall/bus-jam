using UnityEngine;

namespace LevelEditor.BusGrids {
	public class BusCellBehaviour : MonoBehaviour {
		[SerializeField] private new Collider collider;
		private BusCell waitingCell;

		public void Initialize(BusCell levelCell) {
			this.waitingCell = levelCell;
		}

		public Collider GetCollider() => collider;
		public BusCell GetCell() => waitingCell;
	}
}
