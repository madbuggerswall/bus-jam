using Core.CameraSystem.Core;
using Core.LevelGrids;
using Frolics.Contexts;
using Frolics.Input;
using Frolics.Input.Standalone;
using Frolics.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LevelEditor.Tools {
	public class EditorLevelCellSelector : IInitializable, IEditorLevelCellSelector {
		private LevelCell selectedCell;

		// Services
		private IInputManager inputManager;
		private IMainCameraProvider cameraProvider;
		private IEditorCellBehaviourMapper cellBehaviourMapper;

		public void Initialize() {
			inputManager = Context.Resolve<IInputManager>();
			cameraProvider = Context.Resolve<IMainCameraProvider>();
			cellBehaviourMapper = Context.Resolve<IEditorCellBehaviourMapper>();

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

		private bool TrySelectCell(Vector2 pointerPosition, out LevelCell cell) {
			cell = null;
			Ray ray = cameraProvider.GetMainCamera().ScreenPointToRay(pointerPosition);
			if (!Physics.Raycast(ray, out RaycastHit hit))
				return false;

			if (!cellBehaviourMapper.TryGetCellBehaviour(hit.collider, out LevelCellBehaviour cellBehaviour))
				return false;

			cell = cellBehaviour.GetCell();
			return true;
		}

		LevelCell IEditorLevelCellSelector.GetSelectedCell() => selectedCell;
	}
}
