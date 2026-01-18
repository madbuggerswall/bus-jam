using System.Collections.Generic;
using Frolics.Grids.SpatialHelpers;

namespace Core.PathFinding {
	public interface IPathFinder {
		public void OnGridModified();
		public List<SquareCoord> GetPath(SquareCoord sourceCoord);
	}
}