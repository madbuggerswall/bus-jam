using Core.CameraSystem.Core;
using Frolics.Contexts;
using Frolics.Input;
using Frolics.Input.Standalone;
using Frolics.Signals;
using Frolics.Utilities;
using LevelEditor.BusGrids;
using LevelEditor.EditorInput;
using LevelEditor.UI.Signals;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LevelEditor.UI {
	public class EditorBusCellSelector : IInitializable, IEditorBusCellSelector {
		private BusCell selectedCell;

		// Services
		private ISignalBus signalBus;
		private IInputManager inputManager;
		private IMainCameraProvider cameraProvider;
		private IEditorBusCellBehaviourMapper cellBehaviourMapper;

		public void Initialize() {
			signalBus = Context.Resolve<ISignalBus>();
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
			if (!Physics.Raycast(ray, out RaycastHit hit)) {
				signalBus.Fire(new SelectedBusCellChangeSignal(cell));
				return false;
			}

			if (!cellBehaviourMapper.TryGetCellBehaviour(hit.collider, out BusCellBehaviour cellBehaviour)) {
				signalBus.Fire(new SelectedBusCellChangeSignal(cell));
				return false;
			}

			cell = cellBehaviour.GetCell();
			signalBus.Fire(new SelectedBusCellChangeSignal(cell));
			return true;
		}

		BusCell IEditorBusCellSelector.GetSelectedCell() => selectedCell;
	}
}
