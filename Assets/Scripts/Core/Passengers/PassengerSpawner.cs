using System;
using Core.Data;
using Core.LevelGrids;
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

		// Services
		private IGridElementFactory elementFactory;
		private IPassengerColorManager colorManager;

		void IInitializable.Initialize() {
			colorManager = Context.Resolve<IPassengerColorManager>();
			elementFactory = Context.Resolve<IGridElementFactory>();
		}

		void IPassengerSpawner.SpawnPassengers(LevelData levelData, LevelGrid grid) {
			PassengerData[] passengerDTOs = levelData.GetPassengers();
			for (int i = 0; i < passengerDTOs.Length; i++) {
				PassengerData passengerDTO = passengerDTOs[i];
				if (!grid.TryGetCell(passengerDTO.GetLocalCoord(), out LevelCell cell))
					continue;

				Passenger passengerPrefab = GetPrefab(passengerDTO.GetPassengerType());
				Passenger passenger = (Passenger) elementFactory.Create(passengerPrefab, grid, cell);
				Material material = colorManager.GetMaterial(passengerDTO.GetPassengerColor());
				passenger.Initialize(material);
			}
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
