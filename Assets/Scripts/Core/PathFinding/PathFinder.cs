using System.Collections.Generic;
using Core.LevelGrids;
using Core.PathFinding.FlowFields;
using Frolics.Contexts;
using Frolics.Grids.SpatialHelpers;
using Frolics.Utilities;

namespace Core.PathFinding {
	public class PathFinder : IInitializable, IPathFinder {
		private FlowFieldBase flowField;
		private LevelGrid levelGrid;
		private SquareCoord targetCoord;

		// Services
		private ILevelGridProvider levelGridProvider;

		void IInitializable.Initialize() {
			levelGridProvider = Context.Resolve<ILevelGridProvider>();

			levelGrid = levelGridProvider.GetGrid();
			targetCoord = new SquareCoord(levelGrid.GetGridSize().x / 2, levelGrid.GetGridSize().y);
			flowField = new VirtualExitFlowField(levelGrid, targetCoord);
			
			flowField.Build();
		}

		void IPathFinder.OnGridModified() {
			flowField.Build();
		}

		List<SquareCoord> IPathFinder.GetPath(SquareCoord sourceCoord) {
			return flowField.GetPath(sourceCoord);
		}

		bool IPathFinder.IsTargetReachable(SquareCoord sourceCoord) {
			return flowField.IsTargetReachable(sourceCoord);
		}
	}
}
