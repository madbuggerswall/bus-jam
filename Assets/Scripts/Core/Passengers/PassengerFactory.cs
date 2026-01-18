using System;
using Core.Data;
using Frolics.Pooling;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Passengers {
	public interface IPassengerFactory {
		public Passenger Create(PassengerData data);
	}

	public class PassengerSpawner {
		private LevelGrid levelGrid;
		public PassengerSpawner(LevelGrid levelGrid) {}		
	}

	public class PassengerFactory : MonoBehaviour, IInitializable, IPassengerFactory {
		[SerializeField] private Passenger defaultPrefab;
		[SerializeField] private Passenger reservedPrefab;
		[SerializeField] private Passenger secretPrefab;
		[SerializeField] private Passenger cloakPrefab;
		[SerializeField] private Passenger ropePrefab;

		private IObjectPool<Passenger> pool;

		void IInitializable.Initialize() {
			pool = new ObjectPool<Passenger>(transform);
		}

		public Passenger Create(PassengerData data) {
			Passenger passenger = pool.Spawn(GetPrefab(data.GetPassengerType()));

			return passenger;
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
