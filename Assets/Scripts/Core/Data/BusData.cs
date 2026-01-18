using System;
using UnityEngine;

namespace Core.Data {
	[Serializable]
	public struct BusData {
		[SerializeField] private PassengerColor passengerColor;
		[SerializeField] private int reservedCount;

		public PassengerColor GetPassengerColor() => passengerColor;
	}
}