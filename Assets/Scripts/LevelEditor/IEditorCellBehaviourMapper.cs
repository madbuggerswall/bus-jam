using Core.LevelGrids;
using UnityEngine;

namespace LevelEditor {
	public interface IEditorCellBehaviourMapper {
		public void MapCellBehavioursByCollider();
		public bool TryGetCellBehaviour(Collider collider, out LevelCellBehaviour cellBehaviour);
	}
}