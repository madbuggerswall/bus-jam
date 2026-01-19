using Frolics.Input.Standalone;

namespace Core.Input {
	public class MouseCellClickHandler : PointerCellClickHandler {
		public MouseCellClickHandler() {
			inputManager.MouseInputHandler.MousePressEvent += OnMousePress;
		}

		private void OnMousePress(MouseData mouseData) => OnPointerPress(mouseData.MousePosition);
	}
}