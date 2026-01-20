using System;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Core.Data {
	[Serializable]
	public struct PassengerDTO {
		[SerializeField] private SquareCoord localCoord;
		[SerializeField] private ColorDefinition colorDefinition;
		[SerializeField] private PassengerDefinition passengerDefinition;

		public PassengerDTO(
			SquareCoord localCoord,
			ColorDefinition colorDefinition,
			PassengerDefinition passengerDefinition
		) {
			this.localCoord = localCoord;
			this.colorDefinition = colorDefinition;
			this.passengerDefinition = passengerDefinition;
		}

		public SquareCoord GetLocalCoord() => localCoord;
		public ColorDefinition GetColorDefinition() => colorDefinition;
		public PassengerDefinition GetPassengerDefinition() => passengerDefinition;
	}
}
