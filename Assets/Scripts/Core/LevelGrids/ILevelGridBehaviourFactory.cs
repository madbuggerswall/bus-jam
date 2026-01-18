using Frolics.Grids;
using UnityEngine;

namespace Core.LevelGrids {
	public interface ILevelGridBehaviourFactory {
		public LevelGridBehaviour Create(Vector2Int gridSize, GridPlane gridPlane);
	}
}