using System.Collections.Generic;
using UnityEngine;

namespace Core.LevelGrids {
	public class LevelGridBehaviour : MonoBehaviour {
		[SerializeField] private Transform cellRoot;

		private List<LevelCellBehaviour> cellBehaviours;

		public void Initialize() {
			cellBehaviours = new List<LevelCellBehaviour>();
		}

		public Transform GetCellRoot() => cellRoot;
		public List<LevelCellBehaviour> GetCellBehaviours() => cellBehaviours;
	}
}
