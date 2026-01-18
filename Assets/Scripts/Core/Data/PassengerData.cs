using System;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Core.Data {
	[Serializable]
	public struct PassengerData {
		[SerializeField] private SquareCoord localCoord;
		[SerializeField] private PassengerColor passengerColor;
		[SerializeField] private PassengerType passengerType;

		public SquareCoord GetLocalCoord() => localCoord;
		public PassengerColor GetPassengerColor() => passengerColor;
		public PassengerType GetPassengerType() => passengerType;
	}
}