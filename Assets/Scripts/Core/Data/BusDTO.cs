using System;
using UnityEngine;

namespace Core.Data {
	[Serializable]
	public struct BusDTO {
		[SerializeField] private ColorDefinition colorDefinition;
		[SerializeField] private int reservedCount;

		public BusDTO(ColorDefinition colorDefinition, int reservedCount) {
			this.colorDefinition = colorDefinition;
			this.reservedCount = reservedCount;
		}

		public ColorDefinition GetColorDefinition() => colorDefinition;
		public int GetReservedCount() => reservedCount;
	}
}
