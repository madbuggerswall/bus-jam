using System;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Core.Data {
	[Serializable]
	public struct CellDTO {
		[SerializeField] private CellType cellType;
		[SerializeField] private SquareCoord localCoord;

		public CellType GetCellType() => cellType;
		public SquareCoord GetLocalCoord() => localCoord;
	}
}