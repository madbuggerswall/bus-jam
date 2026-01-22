using System.Collections.Generic;
using Core.LevelGrids;
using Frolics.Contexts;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Input {
	public class CellBehaviourMapper : IInitializable, ICellBehaviourMapper {
		private Dictionary<Collider, LevelCellBehaviour> cellBehaviourMap;

		// Services
		private ILevelGridBehaviourProvider levelGridBehaviourProvider;

		void IInitializable.Initialize() {
			levelGridBehaviourProvider = Context.Resolve<ILevelGridBehaviourProvider>();
			cellBehaviourMap = MapCellBehavioursByCollider();
		}

		private Dictionary<Collider, LevelCellBehaviour> MapCellBehavioursByCollider() {
			Dictionary<Collider, LevelCellBehaviour> cellBehaviourMap = new();
			List<LevelCellBehaviour> cellBehaviours = levelGridBehaviourProvider.GetGridBehaviour().GetCellBehaviours();
			for (int i = 0; i < cellBehaviours.Count; i++)
				cellBehaviourMap.Add(cellBehaviours[i].GetOccupiedCellCollider(), cellBehaviours[i]);

			return cellBehaviourMap;
		}

		bool ICellBehaviourMapper.TryGetCellBehaviour(Collider collider, out LevelCellBehaviour cellBehaviour) {
			return cellBehaviourMap.TryGetValue(collider, out cellBehaviour);
		}
	}
}
