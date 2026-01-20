using System;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Data {
	public class PassengerColorManager : MonoBehaviour, IInitializable, IPassengerColorManager {
		[SerializeField] private ColorDefinition blue;
		[SerializeField] private ColorDefinition brown;
		[SerializeField] private ColorDefinition cyan;
		[SerializeField] private ColorDefinition green;
		[SerializeField] private ColorDefinition orange;
		[SerializeField] private ColorDefinition pink;
		[SerializeField] private ColorDefinition purple;
		[SerializeField] private ColorDefinition red;
		[SerializeField] private ColorDefinition white;
		[SerializeField] private ColorDefinition yellow;

		public void Initialize() { }

		public ColorDefinition GetColorDefinition(PassengerColor color) {
			return color switch {
				PassengerColor.Blue => blue,
				PassengerColor.Brown => brown,
				PassengerColor.Cyan => cyan,
				PassengerColor.Green => green,
				PassengerColor.Orange => orange,
				PassengerColor.Pink => pink,
				PassengerColor.Purple => purple,
				PassengerColor.Red => red,
				PassengerColor.White => white,
				PassengerColor.Yellow => yellow,
				_ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
			};
		}
	}
}
