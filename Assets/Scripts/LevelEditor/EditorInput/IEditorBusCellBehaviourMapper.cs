using LevelEditor.BusGrids;
using UnityEngine;

namespace LevelEditor {
	public interface IEditorBusCellBehaviourMapper {
		public void MapCellBehavioursByCollider();
		public bool TryGetCellBehaviour(Collider collider, out BusCellBehaviour cellBehaviour);
	}
}