using System.Collections.Generic;
using Core.LevelGrids;
using Frolics.Contexts;
using Frolics.Utilities;
using UnityEngine;

namespace LevelEditor.EditorInput {
	public class EditorCellBehaviourMapper : IInitializable, IEditorCellBehaviourMapper {
		private readonly Dictionary<Collider, LevelCellBehaviour> cellBehavioursByColliders = new();
		private readonly Dictionary<LevelCell, LevelCellBehaviour> cellBehavioursByCells = new();

		// Services
		private ILevelGridBehaviourProvider levelGridBehaviourProvider;

		void IInitializable.Initialize() {
			levelGridBehaviourProvider = Context.Resolve<ILevelGridBehaviourProvider>();
		}

		void IEditorCellBehaviourMapper.MapCellBehavioursByCollider() {
			cellBehavioursByColliders.Clear();
			List<LevelCellBehaviour> cellBehaviours = levelGridBehaviourProvider.GetGridBehaviour().GetCellBehaviours();
			for (int i = 0; i < cellBehaviours.Count; i++) {
				cellBehavioursByColliders.Add(cellBehaviours[i].GetOccupiedCellCollider(), cellBehaviours[i]);
				cellBehavioursByColliders.Add(cellBehaviours[i].GetEmptyCellCollider(), cellBehaviours[i]);
			}
		}

		void IEditorCellBehaviourMapper.MapCellBehavioursByCells() {
			cellBehavioursByCells.Clear();
			List<LevelCellBehaviour> cellBehaviours = levelGridBehaviourProvider.GetGridBehaviour().GetCellBehaviours();
			for (int i = 0; i < cellBehaviours.Count; i++)
				cellBehavioursByCells.Add(cellBehaviours[i].GetCell(), cellBehaviours[i]);
		}

		bool IEditorCellBehaviourMapper.TryGetCellBehaviour(Collider collider, out LevelCellBehaviour cellBehaviour) {
			return cellBehavioursByColliders.TryGetValue(collider, out cellBehaviour);
		}

		bool IEditorCellBehaviourMapper.TryGetCellBehaviour(LevelCell cell, out LevelCellBehaviour cellBehaviour) {
			return cellBehavioursByCells.TryGetValue(cell, out cellBehaviour);
		}
	}
}
