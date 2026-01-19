using System.Collections.Generic;
using UnityEngine;

namespace Core.Waiting.Grids {
	public class WaitingGridBehaviour : MonoBehaviour {
		[SerializeField] private Transform cellRoot;

		private readonly List<WaitingCellBehaviour> cellBehaviours = new();

		public void Initialize() {
			cellBehaviours.Clear();
		}

		public void AddCellBehaviour(WaitingCellBehaviour cellBehaviour) => cellBehaviours.Add(cellBehaviour);

		// Getters
		public Transform GetCellRoot() => cellRoot;
		public List<WaitingCellBehaviour> GetCellBehaviours() => cellBehaviours;
	}
}