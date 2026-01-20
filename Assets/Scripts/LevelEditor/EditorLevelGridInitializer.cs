using System.Collections.Generic;
using Core.LevelGrids;
using Frolics.Contexts;
using Frolics.Grids;
using Frolics.Utilities;
using UnityEngine;

namespace LevelEditor {
	public class EditorLevelGridInitializer : MonoBehaviour,
		IInitializable,
		ILevelGridBehaviourProvider,
		ILevelGridProvider {
		[SerializeField] private Vector2Int gridSize;

		private LevelGrid grid;
		private LevelGridBehaviour gridBehaviour;

		// Services
		// private ILevelLoader levelLoader;
		private ILevelGridBehaviourFactory gridBehaviourFactory;
		private ILevelCellBehaviourFactory cellBehaviourFactory;
		private IEditorCellBehaviourMapper cellBehaviourMapper;

		void IInitializable.Initialize() {
			// levelLoader = Context.Resolve<ILevelLoader>();
			gridBehaviourFactory = Context.Resolve<ILevelGridBehaviourFactory>();
			cellBehaviourFactory = Context.Resolve<ILevelCellBehaviourFactory>();
			cellBehaviourMapper = Context.Resolve<IEditorCellBehaviourMapper>();

			// Create Grid & GridBehaviour
			gridBehaviour = gridBehaviourFactory.Create();
			grid = CreateLevelGrid();
			cellBehaviourFactory.CreateCellBehaviours(grid, gridBehaviour);
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

		[ContextMenu("Create Grid")]
		private void CreateGrid() {
			if (gridBehaviour != null) {
				List<LevelCellBehaviour> cellBehaviours = gridBehaviour.GetCellBehaviours();
				for (int index = 0; index < cellBehaviours.Count; index++)
					cellBehaviourFactory.Despawn(cellBehaviours[index]);

				gridBehaviourFactory.Despawn(gridBehaviour);
			}

			gridBehaviour = gridBehaviourFactory.Create();
			grid = CreateLevelGrid();
			cellBehaviourFactory.CreateCellBehaviours(grid, gridBehaviour);
			cellBehaviourMapper.MapCellBehavioursByCollider();
		}
	}
}
