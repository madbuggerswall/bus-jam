using Core.Passengers;
using Core.Waiting.Grids;
using Frolics.Contexts;
using Frolics.Utilities;

namespace Core.Waiting {
	// public interface IWaitingAreaManager {
	// 	public bool TryPlacePassenger(Passenger passenger, out );
	// }
	//
	// public class WaitingAreaManager : IInitializable, IWaitingAreaManager {
	// 	// Services
	// 	private IWaitingGridProvider gridProvider;
	// 	private IWaitingGridBehaviourProvider gridBehaviourProvider;
	//
	// 	void IInitializable.Initialize() {
	// 		gridProvider = Context.Resolve<IWaitingGridProvider>();
	// 		gridBehaviourProvider = Context.Resolve<IWaitingGridBehaviourProvider>();
	// 	}
	//
	// 	void IWaitingAreaManager.TryPlacePassenger(Passenger passenger) {
	// 		WaitingGrid grid = gridProvider.GetGrid();
	// 		if (!grid.HasEmptyCells())
	// 			return;
	//
	// 		grid.PlaceToNextEmptyCell(passenger);
	// 	}
	// }
}
