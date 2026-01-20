using System.Collections.Generic;
using Frolics.Contexts;
using Frolics.Grids;
using Frolics.Utilities;
using LevelEditor.BusGrids;
using UnityEngine;

namespace LevelEditor {
	public class BusGridInitializer : MonoBehaviour, IInitializable, IBusGridBehaviourProvider, IBusGridProvider {
		[SerializeField] private Vector2Int gridSize = new Vector2Int(8, 2);

		private BusGrid grid;
		private BusGridBehaviour gridBehaviour;

		// Services
		private IBusGridBehaviourFactory gridBehaviourFactory;
		private IBusCellBehaviourFactory cellBehaviourFactory;
		private IEditorBusCellBehaviourMapper cellBehaviourMapper;

		void IInitializable.Initialize() {
			gridBehaviourFactory = Context.Resolve<IBusGridBehaviourFactory>();
			cellBehaviourFactory = Context.Resolve<IBusCellBehaviourFactory>();
			cellBehaviourMapper = Context.Resolve<IEditorBusCellBehaviourMapper>();
		}

		private BusGrid CreateBusGrid() {
			const GridPlane gridPlane = GridPlane.XZ;
			const float cellDiameter = 1f;

			Vector3 pivotLocalPos = new(-gridSize.x / 2f + cellDiameter / 2, 0, 0);
			BusGrid grid = new(gridBehaviour.transform, pivotLocalPos, gridSize, cellDiameter, gridPlane);
			return grid;
		}

		BusGridBehaviour IBusGridBehaviourProvider.GetGridBehaviour() => gridBehaviour;
		BusGrid IBusGridProvider.GetGrid() => grid;

		[ContextMenu("Create Grid")]
		public void CreateGrid() {
			DespawnFormerGrid();

			gridBehaviour = gridBehaviourFactory.Create();
			grid = CreateBusGrid();
			cellBehaviourFactory.CreateCellBehaviours(grid, gridBehaviour);
			cellBehaviourMapper.MapCellBehavioursByCollider();
		}

		private void DespawnFormerGrid() {
			if (gridBehaviour == null)
				return;

			List<BusCellBehaviour> cellBehaviours = gridBehaviour.GetCellBehaviours();
			for (int index = 0; index < cellBehaviours.Count; index++)
				cellBehaviourFactory.Despawn(cellBehaviours[index]);

			gridBehaviourFactory.Despawn(gridBehaviour);
		}
	}
}
