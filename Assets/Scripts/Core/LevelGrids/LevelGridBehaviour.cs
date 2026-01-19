using System.Collections.Generic;
using UnityEngine;

namespace Core.LevelGrids {
	public class LevelGridBehaviour : MonoBehaviour {
		[SerializeField] private Transform cellRoot;

		private readonly List<LevelCellBehaviour> cellBehaviours = new();

		public void Initialize() {
			cellBehaviours.Clear();
		}

		public void AddCellBehaviour(LevelCellBehaviour cellBehaviour) => cellBehaviours.Add(cellBehaviour);

		// Getters
		public Transform GetCellRoot() => cellRoot;
		public List<LevelCellBehaviour> GetCellBehaviours() => cellBehaviours;
	}
}
