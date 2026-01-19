using Frolics.Input.Mobile;

namespace Core.Input {
	public class TouchCellClickHandler : PointerCellClickHandler {
		public TouchCellClickHandler() {
			inputManager.TouchInputHandler.TouchPressEvent += OnTouchPress;
		}

		private void OnTouchPress(TouchData touchData) => OnPointerPress(touchData.PressPosition);
	}
}