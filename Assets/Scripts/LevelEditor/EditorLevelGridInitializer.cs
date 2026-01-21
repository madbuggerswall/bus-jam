using System.Collections.Generic;
using Core.GridElements;
using Core.LevelGrids;
using Frolics.Contexts;
using Frolics.Grids;
using Frolics.Utilities;
using UnityEngine;

namespace LevelEditor {
	public class EditorLevelGridInitializer : MonoBehaviour,
		IInitializable,
		IEditorLevelGridInitializer,
		ILevelGridBehaviourProvider,
		ILevelGridProvider {
		[SerializeField] private Vector2Int gridSize = new Vector2Int(6, 6);

		private LevelGrid grid;
		private LevelGridBehaviour gridBehaviour;

		// Services
		private IGridElementFactory gridElementFactory;
		private ILevelGridBehaviourFactory gridBehaviourFactory;
		private ILevelCellBehaviourFactory cellBehaviourFactory;
		private IEditorCellBehaviourMapper cellBehaviourMapper;

		void IInitializable.Initialize() {
			gridBehaviourFactory = Context.Resolve<ILevelGridBehaviourFactory>();
			cellBehaviourFactory = Context.Resolve<ILevelCellBehaviourFactory>();
			cellBehaviourMapper = Context.Resolve<IEditorCellBehaviourMapper>();
		}

		// TODO Remove ContextMenu
		[ContextMenu("Create Grid")]
		void IEditorLevelGridInitializer.CreateGrid() {
			DespawnElements();
			DespawnGrid();

			gridBehaviour = gridBehaviourFactory.Create();
			grid = CreateLevelGrid();
			cellBehaviourFactory.CreateCellBehaviours(grid, gridBehaviour);

			// TODO Single method mapping
			cellBehaviourMapper.MapCellBehavioursByCollider();
			cellBehaviourMapper.MapCellBehavioursByCells();
		}

		void IEditorLevelGridInitializer.SetGridSize(Vector2Int gridSize) => this.gridSize = gridSize;

		private void DespawnGrid() {
			if (gridBehaviour == null)
				return;

			List<LevelCellBehaviour> cellBehaviours = gridBehaviour.GetCellBehaviours();
			for (int index = 0; index < cellBehaviours.Count; index++)
				cellBehaviourFactory.Despawn(cellBehaviours[index]);

			gridBehaviourFactory.Despawn(gridBehaviour);
		}

		private void DespawnElements() {
			if (grid == null)
				return;

			Dictionary<GridElement, LevelCell> elements = grid.GetElements();
			foreach ((GridElement element, _) in elements)
				gridElementFactory.Despawn(element);
		}

		private LevelGrid CreateLevelGrid() {
			const GridPlane gridPlane = GridPlane.XZ;
			const float cellDiameter = 1f;

			Vector3 pivotLocalPos = new(-gridSize.x / 2f + cellDiameter / 2, 0, -gridSize.y);
			LevelGrid grid = new(gridBehaviour.transform, pivotLocalPos, gridSize, cellDiameter, gridPlane);
			return grid;
		}

		LevelGridBehaviour ILevelGridBehaviourProvider.GetGridBehaviour() => gridBehaviour;
		LevelGrid ILevelGridProvider.GetGrid() => grid;
	}
}
