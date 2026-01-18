using System.Collections.Generic;
using Frolics.Grids.SpatialHelpers;
using Frolics.Utilities;

namespace Core.PathFinding {
	public class PathFinder : IInitializable, IPathFinder {
		private FlowFieldBase flowField;
		private LevelGrid levelGrid;

		void IInitializable.Initialize() {
			// TODO Assign levelGrid
			SquareCoord targetCoord = new(levelGrid.GetGridSize().x / 2, levelGrid.GetGridSize().y);
			flowField = new VirtualExitFlowField(levelGrid, targetCoord);
		}

		void IPathFinder.OnGridModified() {
			flowField.Build();
		}

		List<SquareCoord> IPathFinder.GetPath(SquareCoord sourceCoord) {
			return flowField.GetPath(sourceCoord);
		}
	}
}
