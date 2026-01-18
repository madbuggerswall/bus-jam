using System;
using Core.CameraSystem.Core;
using Frolics.Contexts;
using Frolics.Input;
using Frolics.Input.Mobile;
using Frolics.Input.Standalone;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Input {
	public class PassengerClickHandler : IInitializable {
		private IPointerClickHandler clickHandler;

		void IInitializable.Initialize() {
			IClickHandlerFactory clickHandlerFactory = new ClickHandlerFactory();
			clickHandler = clickHandlerFactory.Create();
		}
	}

	public interface IPointerClickHandler { }

	public interface IClickable {
		public void OnClick();
	}

	public interface IClickHandlerFactory {
		public IPointerClickHandler Create();
	}

	public class ClickHandlerFactory : IClickHandlerFactory {
		public IPointerClickHandler Create() {
			return Application.platform switch {
				RuntimePlatform.Android or RuntimePlatform.IPhonePlayer => new TouchClickHandler(),
				RuntimePlatform.LinuxEditor or RuntimePlatform.OSXEditor => new MouseClickHandler(),
				_ => throw new ArgumentOutOfRangeException()
			};
		}
	}

	public abstract class PointerClickHandler : IPointerClickHandler {
		// Services
		protected readonly IInputManager inputManager;
		private readonly IMainCameraProvider cameraProvider;

		protected PointerClickHandler() {
			inputManager = Context.Resolve<IInputManager>();
			cameraProvider = Context.Resolve<IMainCameraProvider>();
		}

		// Sandbox methods 
		protected void OnPointerPress(Vector2 pointerPosition) {
			Ray ray = cameraProvider.GetMainCamera().ScreenPointToRay(pointerPosition);
			if (!Physics.Raycast(ray, out RaycastHit hit))
				return;

			// IDEA ClickableManager caches a <Collider,Clickable> dictionary
			IClickable clickable = hit.collider.GetComponent<IClickable>();
			clickable?.OnClick();
		}

		protected void OnPointerDrag(Vector2 pointerPosition) { }

		// IDEA Cancel the press if dragged away from clickable
		protected void OnPointerRelease(Vector2 pointerPosition) { }
	}

	public class MouseClickHandler : PointerClickHandler {
		public MouseClickHandler() {
			inputManager.MouseInputHandler.MousePressEvent += OnMousePress;
			inputManager.MouseInputHandler.MouseDragEvent += OnMouseDrag;
			inputManager.MouseInputHandler.MouseReleaseEvent += OnMouseRelease;
		}

		private void OnMousePress(MouseData mouseData) => OnPointerPress(mouseData.MousePosition);
		private void OnMouseDrag(MouseData mouseData) => OnPointerDrag(mouseData.MousePosition);
		private void OnMouseRelease(MouseData mouseData) => OnPointerRelease(mouseData.MousePosition);
	}

	public class TouchClickHandler : PointerClickHandler {
		public TouchClickHandler() {
			inputManager.TouchInputHandler.TouchPressEvent += OnTouchPress;
			inputManager.TouchInputHandler.TouchDragEvent += OnTouchDrag;
			inputManager.TouchInputHandler.TouchReleaseEvent += OnTouchRelease;
		}

		private void OnTouchPress(TouchData touchData) => OnPointerPress(touchData.PressPosition);
		private void OnTouchDrag(TouchData touchData) => OnPointerDrag(touchData.PressPosition);
		private void OnTouchRelease(TouchData touchData) => OnPointerRelease(touchData.PressPosition);
	}
}
