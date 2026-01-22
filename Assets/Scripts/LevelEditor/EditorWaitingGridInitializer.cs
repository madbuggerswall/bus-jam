using System.Collections.Generic;
using Core.Waiting.Grids;
using Frolics.Contexts;
using Frolics.Grids;
using Frolics.Utilities;
using UnityEngine;

namespace LevelEditor {
	public class EditorWaitingGridInitializer : MonoBehaviour,
		IInitializable,
		IEditorWaitingGridInitializer,
		IWaitingGridBehaviourProvider,
		IWaitingGridProvider {
		[SerializeField] private Vector2Int gridSize = new Vector2Int(7, 1);

		private WaitingGrid grid;
		private WaitingGridBehaviour gridBehaviour;

		// Services
		private IWaitingGridBehaviourFactory gridBehaviourFactory;
		private IWaitingCellBehaviourFactory cellBehaviourFactory;

		void IInitializable.Initialize() {
			gridBehaviourFactory = Context.Resolve<IWaitingGridBehaviourFactory>();
			cellBehaviourFactory = Context.Resolve<IWaitingCellBehaviourFactory>();
		}

		void IEditorWaitingGridInitializer.CreateGrid() {
			DespawnGrid();

			gridBehaviour = gridBehaviourFactory.Create();
			grid = CreateWaitingGrid();
			cellBehaviourFactory.CreateCellBehaviours(grid, gridBehaviour);
		}

		void IEditorWaitingGridInitializer.SetGridSize(Vector2Int gridSize) => this.gridSize = gridSize;

		private void DespawnGrid() {
			if (gridBehaviour == null)
				return;

			List<WaitingCellBehaviour> cellBehaviours = gridBehaviour.GetCellBehaviours();
			for (int index = 0; index < cellBehaviours.Count; index++)
				cellBehaviourFactory.Despawn(cellBehaviours[index]);

			gridBehaviourFactory.Despawn(gridBehaviour);
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
	}
}
