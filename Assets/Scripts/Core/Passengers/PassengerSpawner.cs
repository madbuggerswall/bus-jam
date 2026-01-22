using Core.Data;
using Core.GridElements;
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

		void IInitializable.Initialize() {
			levelLoader = Context.Resolve<ILevelLoader>();
			elementFactory = Context.Resolve<IGridElementFactory>();
		}

		void IPassengerSpawner.SpawnPassengers(LevelGrid grid) {
			LevelDTO levelDTO = levelLoader.GetLevelData();
			PassengerDTO[] passengerDTOs = levelDTO.GetPassengerDTOs();

			for (int i = 0; i < passengerDTOs.Length; i++) {
				PassengerDTO passengerDTO = passengerDTOs[i];
				if (!grid.TryGetCell(passengerDTO.GetLocalCoord(), out LevelCell cell))
					continue;

				Passenger passengerPrefab = passengerDTO.GetPassengerDefinition().GetPrefab();
				Passenger passenger = (Passenger) elementFactory.Create(passengerPrefab, grid, cell);
				ColorDefinition colorDefinition = passengerDTO.GetColorDefinition();
				passenger.Initialize(colorDefinition);
			}
		}
	}
}
