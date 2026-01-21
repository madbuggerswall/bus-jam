using Core.LevelGrids;
using Core.Passengers;

namespace Core.Mechanics {
	public interface IRuleService {
		public void OnPassengerSelected(Passenger passenger, LevelCell cell);
	}
}