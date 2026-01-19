using Core.Data;

namespace Core.Buses {
	public interface IBusFactory {
		public Bus CreateBus(BusData busData);
	}
}