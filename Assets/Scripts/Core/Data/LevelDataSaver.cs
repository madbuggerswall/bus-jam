using System.Collections.Generic;
using Core.LevelGrids;
using Core.Passengers;
using Core.Waiting.Grids;
using Frolics.Contexts;
using Frolics.Grids.SpatialHelpers;
using Frolics.Utilities;
using LevelEditor;

namespace Core.Data {
	public class LevelDataSaver : IInitializable {
		// Services
		private ILevelGridProvider levelGridProvider;
		private IWaitingGridProvider waitingGridProvider;
		private IBusGridProvider busGridProvider;
		private ILevelTimeProvider levelTimeProvider;

		void IInitializable.Initialize() {
			levelGridProvider = Context.Resolve<ILevelGridProvider>();
			waitingGridProvider = Context.Resolve<IWaitingGridProvider>();
			busGridProvider = Context.Resolve<IBusGridProvider>();
			levelTimeProvider = Context.Resolve<ILevelTimeProvider>();
		}

		public void Save() { }

		private void PassengerDataStuff() {
			List<PassengerDTO> passengerDTOs = new();
			LevelGrid grid = levelGridProvider.GetGrid();
			LevelCell[] cells = grid.GetCells();
			
			for (int i = 0; i < cells.Length; i++) {
				LevelCell cell =  cells[i];
				if(!cell.HasElement())
					continue;

				if (cell.GetGridElement() is Passenger passenger) {
					ColorDefinition colorDefinition = passenger.GetColorDefinition();
					SquareCoord localCoord = cell.GetCoord();
				}
				
			}
		}
	}
}