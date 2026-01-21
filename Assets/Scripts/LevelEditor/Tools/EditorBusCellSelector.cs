using Core.CameraSystem.Core;
using Frolics.Contexts;
using Frolics.Input;
using Frolics.Input.Standalone;
using Frolics.Utilities;
using LevelEditor.BusGrids;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LevelEditor.Tools {
	public class EditorBusCellSelector : IInitializable, IEditorBusCellSelector {
		private BusCell selectedCell;

		// Services
		private IInputManager inputManager;
		private IMainCameraProvider cameraProvider;
		private IEditorBusCellBehaviourMapper cellBehaviourMapper;

		public void Initialize() {
			inputManager = Context.Resolve<IInputManager>();
			cameraProvider = Context.Resolve<IMainCameraProvider>();
			cellBehaviourMapper = Context.Resolve<IEditorBusCellBehaviourMapper>();

			inputManager.MouseInputHandler.MousePressEvent += OnMousePress;
		}

		private void OnMousePress(MouseData data) {
			if (data.ButtonControl == Mouse.current.leftButton) {
				OnPrimaryPress(data.MousePosition);
			}

			if (data.ButtonControl == Mouse.current.rightButton) { }
		}

		private void OnPrimaryPress(Vector2 pointerPosition) {
			TrySelectCell(pointerPosition, out selectedCell);
		}

		private bool TrySelectCell(Vector2 pointerPosition, out BusCell cell) {
			cell = null;

			Ray ray = cameraProvider.GetMainCamera().ScreenPointToRay(pointerPosition);
			if (!Physics.Raycast(ray, out RaycastHit hit))
				return false;

			if (!cellBehaviourMapper.TryGetCellBehaviour(hit.collider, out BusCellBehaviour cellBehaviour))
				return false;

			cell = cellBehaviour.GetCell();
			return true;
		}

		BusCell IEditorBusCellSelector.GetSelectedCell() => selectedCell;
	}
}
