using System;
using UnityEngine;

namespace Core.Data {
	[Serializable]
	public struct BusDTO {
		[SerializeField] private ColorDefinition colorDefinition;
		[SerializeField] private int capacity;
		[SerializeField] private int reservedCapacity;

		public BusDTO(ColorDefinition colorDefinition, int capacity, int reservedCapacity) {
			this.colorDefinition = colorDefinition;
			this.capacity = capacity;
			this.reservedCapacity = reservedCapacity;
		}

		public ColorDefinition GetColorDefinition() => colorDefinition;
		public int GetCapacity() => reservedCapacity;
		public int GetReservedCapacity() => reservedCapacity;
	}
}
