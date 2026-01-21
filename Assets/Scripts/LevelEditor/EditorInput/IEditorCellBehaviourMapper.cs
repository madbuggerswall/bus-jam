using Core.LevelGrids;
using UnityEngine;

namespace LevelEditor.EditorInput {
	public interface IEditorCellBehaviourMapper {
		public void MapCellBehavioursByCollider();
		public bool TryGetCellBehaviour(Collider collider, out LevelCellBehaviour cellBehaviour);
		void MapCellBehavioursByCells();
		bool TryGetCellBehaviour(LevelCell cell, out LevelCellBehaviour cellBehaviour);
	}
}