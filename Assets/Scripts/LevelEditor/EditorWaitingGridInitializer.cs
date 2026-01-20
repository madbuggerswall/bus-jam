using System.Collections.Generic;
using Core.Waiting.Grids;
using Frolics.Contexts;
using Frolics.Grids;
using Frolics.Utilities;
using UnityEngine;

namespace LevelEditor {
	public class EditorWaitingGridInitializer : MonoBehaviour,
		IInitializable,
		IWaitingGridBehaviourProvider,
		IWaitingGridProvider {
		[SerializeField] private Vector2Int gridSize;

		private WaitingGrid grid;
		private WaitingGridBehaviour gridBehaviour;

		// Services
		private IWaitingGridBehaviourFactory gridBehaviourFactory;
		private IWaitingCellBehaviourFactory cellBehaviourFactory;

		void IInitializable.Initialize() {
			gridBehaviourFactory = Context.Resolve<IWaitingGridBehaviourFactory>();
			cellBehaviourFactory = Context.Resolve<IWaitingCellBehaviourFactory>();

			// Create Grid & GridBehaviour
			// gridBehaviour = gridBehaviourFactory.Create();
			// grid = CreateGrid();
			// cellBehaviourFactory.CreateCellBehaviours(grid, gridBehaviour);
		}

		private WaitingGrid CreateWaitingGrid() {
			const GridPlane gridPlane = GridPlane.XZ;
			const float cellDiameter = 1f;

			Vector3 pivotLocalPos = new(-gridSize.x / 2f + cellDiameter / 2, 0, -gridSize.y / 2f + cellDiameter / 2);
			WaitingGrid grid = new(gridBehaviour.transform, pivotLocalPos, gridSize, cellDiameter, gridPlane);

			return grid;
		}

		WaitingGridBehaviour IWaitingGridBehaviourProvider.GetWaitingGridBehaviour() => gridBehaviour;
		WaitingGrid IWaitingGridProvider.GetGrid() => grid;
		
		[ContextMenu("Create Grid")]
		private void CreateGrid() {
			if (gridBehaviour != null) {
				List<WaitingCellBehaviour> cellBehaviours = gridBehaviour.GetCellBehaviours();
				for (int index = 0; index < cellBehaviours.Count; index++)
					cellBehaviourFactory.Despawn(cellBehaviours[index]);

				gridBehaviourFactory.Despawn(gridBehaviour);
			}

			gridBehaviour = gridBehaviourFactory.Create();
			grid = CreateWaitingGrid();
			cellBehaviourFactory.CreateCellBehaviours(grid, gridBehaviour);
		}
	}
}
