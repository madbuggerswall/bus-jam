using Core.Data;
using Core.GridElements;
using Core.LevelGrids;
using Core.Passengers;
using Frolics.Contexts;
using Frolics.Utilities;

namespace LevelEditor.SaveSystem {
	public class PassengerDTOSpawner : IInitializable, IPassengerDTOSpawner {
		private IGridElementFactory elementFactory;
		private ILevelGridProvider levelGridProvider;

		void IInitializable.Initialize() {
			elementFactory = Context.Resolve<IGridElementFactory>();
			levelGridProvider = Context.Resolve<ILevelGridProvider>();
		}

		void IPassengerDTOSpawner.SpawnPassenger(PassengerDTO passengerDTO) {
			PassengerDefinition definition = passengerDTO.GetPassengerDefinition();
			Passenger prefab = definition.GetPrefab();
			LevelGrid levelGrid = levelGridProvider.GetGrid();

			if (!levelGrid.TryGetCell(passengerDTO.GetLocalCoord(), out LevelCell cell))
				return;

			GridElement element = elementFactory.Create(prefab, levelGrid, cell);
			if (element is IColorable colorable)
				colorable.SetColorDefinition(passengerDTO.GetColorDefinition());
		}
	}
}