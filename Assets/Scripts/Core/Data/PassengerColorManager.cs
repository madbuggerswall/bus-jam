using System;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Data {
	public class PassengerColorManager : MonoBehaviour, IInitializable, IPassengerColorManager {
		[SerializeField] private Material blue;
		[SerializeField] private Material brown;
		[SerializeField] private Material cyan;
		[SerializeField] private Material green;
		[SerializeField] private Material orange;
		[SerializeField] private Material pink;
		[SerializeField] private Material purple;
		[SerializeField] private Material red;
		[SerializeField] private Material yellow;
		[SerializeField] private Material white;

		public void Initialize() { }
		
		public Material GetMaterial(PassengerColor color) {
			return color switch {
				PassengerColor.Blue => blue,
				PassengerColor.Brown => brown,
				PassengerColor.Cyan => cyan,
				PassengerColor.Green => green,
				PassengerColor.Orange => orange,
				PassengerColor.Pink => pink,
				PassengerColor.Purple => purple,
				PassengerColor.Red => red,
				PassengerColor.Yellow => white,
				PassengerColor.White => white,
				_ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
			};
		}
	}
}
