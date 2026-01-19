using System.Collections.Generic;
using Core.LevelGrids;
using Core.Levels;
using Frolics.Contexts;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Input {
	public class CellBehaviourMapper : IInitializable, ICellBehaviourMapper {
		private Dictionary<Collider, LevelCellBehaviour> cellBehaviourMap;
		
		// Services
		private IGridBehaviourProvider gridBehaviourProvider;

		void IInitializable.Initialize() {
			gridBehaviourProvider = Context.Resolve<IGridBehaviourProvider>();
			cellBehaviourMap = MapCellBehavioursByCollider();
		}

		private Dictionary<Collider, LevelCellBehaviour> MapCellBehavioursByCollider() {
			Dictionary<Collider, LevelCellBehaviour> cellBehaviourMap = new();
			List<LevelCellBehaviour> cellBehaviours = gridBehaviourProvider.GetGridBehaviour().GetCellBehaviours();
			for (int i = 0; i < cellBehaviours.Count; i++)
				cellBehaviourMap.Add(cellBehaviours[i].GetCollider(), cellBehaviours[i]);
			
			return cellBehaviourMap;
		}

		bool ICellBehaviourMapper.TryGetCellBehaviour(Collider collider, out LevelCellBehaviour cellBehaviour) {
			return cellBehaviourMap.TryGetValue(collider, out cellBehaviour);
		}
	}
}