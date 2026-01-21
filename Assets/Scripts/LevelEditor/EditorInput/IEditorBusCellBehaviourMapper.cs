using LevelEditor.BusGrids;
using UnityEngine;

namespace LevelEditor.EditorInput {
	public interface IEditorBusCellBehaviourMapper {
		public void MapCellBehavioursByCollider();
		public bool TryGetCellBehaviour(Collider collider, out BusCellBehaviour cellBehaviour);
	}
}