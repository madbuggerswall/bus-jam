using System.Collections.Generic;
using Frolics.Contexts;
using Frolics.Utilities;
using LevelEditor.BusGrids;
using UnityEngine;

namespace LevelEditor.EditorInput {
	public class EditorBusCellBehaviourMapper : IInitializable, IEditorBusCellBehaviourMapper {
		private readonly Dictionary<Collider, BusCellBehaviour> cellBehaviourMap = new();

		// Services
		private IBusGridBehaviourProvider busGridBehaviourProvider;

		void IInitializable.Initialize() {
			busGridBehaviourProvider = Context.Resolve<IBusGridBehaviourProvider>();
		}

		void IEditorBusCellBehaviourMapper.MapCellBehavioursByCollider() {
			cellBehaviourMap.Clear();
			List<BusCellBehaviour> cellBehaviours = busGridBehaviourProvider.GetGridBehaviour().GetCellBehaviours();
			for (int i = 0; i < cellBehaviours.Count; i++)
				cellBehaviourMap.Add(cellBehaviours[i].GetCollider(), cellBehaviours[i]);
		}

		bool IEditorBusCellBehaviourMapper.TryGetCellBehaviour(Collider collider, out BusCellBehaviour cellBehaviour) {
			return cellBehaviourMap.TryGetValue(collider, out cellBehaviour);
		}
	}
}
