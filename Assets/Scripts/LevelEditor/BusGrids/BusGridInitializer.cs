using System.Collections.Generic;
using Frolics.Contexts;
using Frolics.Grids;
using Frolics.Utilities;
using LevelEditor.EditorBuses;
using LevelEditor.EditorInput;
using UnityEngine;

namespace LevelEditor.BusGrids {
	public class BusGridInitializer : MonoBehaviour,
		IInitializable,
		IBusGridInitializer,
		IBusGridBehaviourProvider,
		IBusGridProvider {
		[SerializeField] private Vector2Int gridSize = new(8, 2);

		private BusGrid grid;
		private BusGridBehaviour gridBehaviour;

		// Services
		private IEditorBusFactory editorBusFactory;
		private IBusGridBehaviourFactory gridBehaviourFactory;
		private IBusCellBehaviourFactory cellBehaviourFactory;
		private IEditorBusCellBehaviourMapper cellBehaviourMapper;

		void IInitializable.Initialize() {
			editorBusFactory = Context.Resolve<IEditorBusFactory>();
			gridBehaviourFactory = Context.Resolve<IBusGridBehaviourFactory>();
			cellBehaviourFactory = Context.Resolve<IBusCellBehaviourFactory>();
			cellBehaviourMapper = Context.Resolve<IEditorBusCellBehaviourMapper>();
		}

		void IBusGridInitializer.CreateGrid() {
			DespawnBuses();
			DespawnGrid();

			gridBehaviour = gridBehaviourFactory.Create();
			grid = CreateBusGrid();
			cellBehaviourFactory.CreateCellBehaviours(grid, gridBehaviour);
			cellBehaviourMapper.MapCellBehavioursByCollider();
		}

		void IBusGridInitializer.SetGridSize(Vector2Int gridSize) => this.gridSize = gridSize;

		private void DespawnGrid() {
			if (gridBehaviour == null)
				return;

			List<BusCellBehaviour> cellBehaviours = gridBehaviour.GetCellBehaviours();
			for (int index = 0; index < cellBehaviours.Count; index++)
				cellBehaviourFactory.Despawn(cellBehaviours[index]);

			gridBehaviourFactory.Despawn(gridBehaviour);
		}

		private void DespawnBuses() {
			if (grid == null)
				return;

			Dictionary<EditorBus, BusCell> buses = grid.GetBuses();
			foreach ((EditorBus editorBus, _) in buses)
				editorBusFactory.Despawn(editorBus);
		}

		private BusGrid CreateBusGrid() {
			const GridPlane gridPlane = GridPlane.XZ;
			const float cellDiameter = 1f;

			Vector3 pivotLocalPos = new(-gridSize.x / 2f + cellDiameter / 2, 0, 0);
			return new BusGrid(gridBehaviour.transform, pivotLocalPos, gridSize, cellDiameter, gridPlane);
		}

		BusGridBehaviour IBusGridBehaviourProvider.GetGridBehaviour() => gridBehaviour;
		BusGrid IBusGridProvider.GetGrid() => grid;
	}
}
