using Core.Data;
using UnityEngine;

namespace Core.LevelGrids {
	public class LevelGridBehaviour : MonoBehaviour {
		[SerializeField] private Transform cellRoot;

		public void Initialize() { }

		public Transform GetCellRoot() => cellRoot;
	}
}
