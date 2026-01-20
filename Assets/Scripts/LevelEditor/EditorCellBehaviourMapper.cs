using System.Collections.Generic;
using Core.LevelGrids;
using Frolics.Contexts;
using Frolics.Utilities;
using UnityEngine;

namespace LevelEditor {
	public class EditorCellBehaviourMapper : IInitializable, IEditorCellBehaviourMapper {
		private Dictionary<Collider, LevelCellBehaviour> cellBehaviourMap = new();

		// Services
		private ILevelGridBehaviourProvider levelGridBehaviourProvider;

		void IInitializable.Initialize() {
			levelGridBehaviourProvider = Context.Resolve<ILevelGridBehaviourProvider>();
		}

		void IEditorCellBehaviourMapper.MapCellBehavioursByCollider() {
			cellBehaviourMap.Clear();
			List<LevelCellBehaviour> cellBehaviours = levelGridBehaviourProvider.GetGridBehaviour().GetCellBehaviours();
			for (int i = 0; i < cellBehaviours.Count; i++)
				cellBehaviourMap.Add(cellBehaviours[i].GetCollider(), cellBehaviours[i]);
		}

		bool IEditorCellBehaviourMapper.TryGetCellBehaviour(Collider collider, out LevelCellBehaviour cellBehaviour) {
			return cellBehaviourMap.TryGetValue(collider, out cellBehaviour);
		}
	}
}