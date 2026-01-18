using System;
using Core.Data;
using Frolics.Contexts;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Passengers {
	public class PassengerSpawner : MonoBehaviour, IInitializable, IPassengerSpawner {
		[SerializeField] private Passenger defaultPrefab;
		[SerializeField] private Passenger reservedPrefab;
		[SerializeField] private Passenger secretPrefab;
		[SerializeField] private Passenger cloakPrefab;
		[SerializeField] private Passenger ropePrefab;

		private IGridElementFactory elementFactory;

		void IInitializable.Initialize() {
			elementFactory = Context.Resolve<IGridElementFactory>();
		}

		Passenger IPassengerSpawner.Spawn(PassengerType type, LevelGrid grid, LevelCell cell) {
			return elementFactory.Create(GetPrefab(type), grid, cell) as Passenger;
		}

		private Passenger GetPrefab(PassengerType type) {
			return type switch {
				PassengerType.Default => defaultPrefab,
				PassengerType.Reserved => reservedPrefab,
				PassengerType.Secret => secretPrefab,
				PassengerType.Cloak => cloakPrefab,
				PassengerType.Rope => ropePrefab,
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
			};
		}
	}
}
