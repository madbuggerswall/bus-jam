using System.Collections.Generic;
using Core.LevelGrids;
using Core.Levels;
using Core.PathFinding.FlowFields;
using Frolics.Contexts;
using Frolics.Grids.SpatialHelpers;
using Frolics.Utilities;

namespace Core.PathFinding {
	public class PathFinder : IInitializable, IPathFinder {
		private FlowFieldBase flowField;
		private LevelGrid levelGrid;
		
		// Services
		private IGridProvider  gridProvider;

		void IInitializable.Initialize() {
			gridProvider = Context.Resolve<IGridProvider>();
			
			levelGrid = gridProvider.GetGrid();
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
