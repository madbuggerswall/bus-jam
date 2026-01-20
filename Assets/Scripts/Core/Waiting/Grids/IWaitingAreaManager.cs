using System.Collections.Generic;
using Core.Passengers;

namespace Core.Waiting.Grids {
	public interface IWaitingAreaManager {
		public bool HasEmptySlots();
		public bool TryPlacePassenger(Passenger passenger);
		public void RemovePassenger(Passenger passenger);
		public List<Passenger> GetPassengers();
	}
}