using UnityEngine;

namespace Core.Data {
	public interface IPassengerColorManager {
		public Material GetMaterial(PassengerColor color);
	}
}