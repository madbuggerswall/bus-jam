using Frolics.Utilities;

namespace Core.Input {
	public class PassengerClickHandler : IInitializable {
		private IPointerClickHandler clickHandler;

		void IInitializable.Initialize() {
			IClickHandlerFactory clickHandlerFactory = new ClickHandlerFactory();
			clickHandler = clickHandlerFactory.Create();
		}
	}
}
