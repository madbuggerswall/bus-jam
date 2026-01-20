using System;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Core.Data {
	[Serializable]
	public struct CellDTO {
		[SerializeField] private bool isEmpty;
		[SerializeField] private SquareCoord localCoord;

		public CellDTO(SquareCoord localCoord, bool isEmpty) {
			this.isEmpty = isEmpty;
			this.localCoord = localCoord;
		}

		public bool IsEmpty() => isEmpty;
		public SquareCoord GetLocalCoord() => localCoord;
	}
}
