using Core.LevelGrids;
using UnityEngine;

namespace Core.Input {
	public interface ICellBehaviourMapper {
		public bool TryGetCellBehaviour(Collider collider, out LevelCellBehaviour cellBehaviour);
	}
}