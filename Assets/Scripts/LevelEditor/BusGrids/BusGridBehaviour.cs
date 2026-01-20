using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor.BusGrids {
	public class BusGridBehaviour : MonoBehaviour {
		[SerializeField] private Transform cellRoot;

		private readonly List<BusCellBehaviour> cellBehaviours = new();

		public void Initialize() {
			cellBehaviours.Clear();
		}

		public void AddCellBehaviour(BusCellBehaviour cellBehaviour) => cellBehaviours.Add(cellBehaviour);

		// Getters
		public Transform GetCellRoot() => cellRoot;
		public List<BusCellBehaviour> GetCellBehaviours() => cellBehaviours;
	}
}
