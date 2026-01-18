using System;
using Core.Data;
using Core.GridElements;
using Frolics.Pooling;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Passengers {
	public interface IPassengerFactory {
		public Passenger Create(PassengerType type, LevelGrid grid, LevelCell cell);
	}

	public class PassengerSpawner {
		private LevelGrid levelGrid;
		public PassengerSpawner(LevelGrid levelGrid) { }
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

		Passenger IPassengerFactory.Create(PassengerType type, LevelGrid grid, LevelCell cell) {
			// TODO grid.GetPassengerRoot
			Passenger passenger = pool.Spawn(GetPrefab(type));
			ElementLifecycle lifecycle = new ElementLifecycle(passenger, grid, pool);
			passenger.Initialize(lifecycle);

			Vector3 pivotOffset = passenger.transform.position - passenger.GetPivotWorldPosition();
			passenger.transform.position = grid.GetWorldPosition(cell) + pivotOffset;

			grid.PlaceElementAtCell(cell, passenger);
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

	public interface IGridElementFactory {
		public GridElement Create(LevelGrid grid, LevelCell cell, GridElement prefab);
	}

	public class GridElementFactory : MonoBehaviour, IInitializable, IGridElementFactory {
		private IObjectPool<GridElement> pool;

		void IInitializable.Initialize() {
			pool = new ObjectPool<GridElement>(transform);
		}

		GridElement IGridElementFactory.Create(LevelGrid grid, LevelCell cell, GridElement prefab) {
			// TODO Set element's parent to an elementRoot 
			GridElement element = pool.Spawn(prefab);
			ElementLifecycle lifecycle = new ElementLifecycle(element, grid, pool);
			element.Initialize(lifecycle);

			Vector3 pivotOffset = element.transform.position - element.GetPivotWorldPosition();
			element.transform.position = grid.GetWorldPosition(cell) + pivotOffset;

			grid.PlaceElementAtCell(cell, element);
			return element;
		}
	}
}
