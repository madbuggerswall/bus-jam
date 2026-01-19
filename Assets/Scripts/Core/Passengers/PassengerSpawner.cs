using System;
using Core.Data;
using Core.LevelGrids;
using Core.Levels;
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
		private ILevelLoader levelLoader;
		private IGridElementFactory elementFactory;
		private IPassengerColorManager colorManager;
		private ILevelGridProvider gridProvider;

		void IInitializable.Initialize() {
			levelLoader = Context.Resolve<ILevelLoader>();
			colorManager = Context.Resolve<IPassengerColorManager>();
			elementFactory = Context.Resolve<IGridElementFactory>();
			gridProvider = Context.Resolve<ILevelGridProvider>();

			SpawnPassengers();
		}

		public void SpawnPassengers() {
			LevelData levelData = levelLoader.GetLevelData();
			PassengerData[] passengerDTOs = levelData.GetPassengers();
			LevelGrid grid = gridProvider.GetGrid();

			for (int i = 0; i < passengerDTOs.Length; i++) {
				PassengerData passengerDTO = passengerDTOs[i];
				if (!grid.TryGetCell(passengerDTO.GetLocalCoord(), out LevelCell cell))
					continue;

				Passenger passengerPrefab = GetPrefab(passengerDTO.GetPassengerType());
				Passenger passenger = (Passenger) elementFactory.Create(passengerPrefab, grid, cell);
				PassengerColor color = passengerDTO.GetPassengerColor();
				Material material = colorManager.GetMaterial(color);
				passenger.Initialize(color, material);
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
