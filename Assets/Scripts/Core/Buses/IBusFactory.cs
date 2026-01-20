using Core.Data;

namespace Core.Buses {
	public interface IBusFactory {
		public Bus CreateBus(BusDTO busDTO);
	}
}