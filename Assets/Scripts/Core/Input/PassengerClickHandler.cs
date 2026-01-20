using Frolics.Utilities;

namespace Core.Input {
	public class PassengerClickHandler : IInitializable {
		private IPointerCellClickHandler cellClickHandler;

		void IInitializable.Initialize() {
			IClickHandlerFactory clickHandlerFactory = new ClickHandlerFactory();
			cellClickHandler = clickHandlerFactory.Create();
		}
	}
}
