using System;
using System.Collections.Generic;
using Core.CameraSystem.Core;
using Core.LevelGrids;
using Core.Passengers;
using Frolics.Contexts;
using Frolics.Input;
using Frolics.Input.Mobile;
using Frolics.Input.Standalone;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Input {
	public interface ICellBehaviourMapper {
		public bool TryGetCellBehaviour(Collider collider, out LevelCellBehaviour cellBehaviour);
	}

	public class CellBehaviourMapper : IInitializable, ICellBehaviourMapper {
		private Dictionary<Collider, LevelCellBehaviour> cellBehaviourMap;
		private LevelGridBehaviour gridBehaviour;

		void IInitializable.Initialize() {
			cellBehaviourMap = new Dictionary<Collider, LevelCellBehaviour>();
			List<LevelCellBehaviour> cellBehaviours = gridBehaviour.GetCellBehaviours();
			for (int i = 0; i < cellBehaviours.Count; i++)
				cellBehaviourMap.Add(cellBehaviours[i].GetCollider(), cellBehaviours[i]);
		}

		bool ICellBehaviourMapper.TryGetCellBehaviour(Collider collider, out LevelCellBehaviour cellBehaviour) {
			return cellBehaviourMap.TryGetValue(collider, out cellBehaviour);
		}
	}

	public class PassengerClickHandler : IInitializable {
		private IPointerClickHandler clickHandler;

		void IInitializable.Initialize() {
			IClickHandlerFactory clickHandlerFactory = new ClickHandlerFactory();
			clickHandler = clickHandlerFactory.Create();
		}
	}

	public interface IPointerClickHandler { }

	public interface IClickHandlerFactory {
		public IPointerClickHandler Create();
	}

	public class ClickHandlerFactory : IClickHandlerFactory {
		public IPointerClickHandler Create() {
			return Application.platform switch {
				RuntimePlatform.Android or RuntimePlatform.IPhonePlayer => new TouchCellClickHandler(),
				RuntimePlatform.LinuxEditor or RuntimePlatform.OSXEditor => new MouseCellClickHandler(),
				_ => throw new ArgumentOutOfRangeException()
			};
		}
	}

	public abstract class PointerCellClickHandler : IPointerClickHandler {
		// Services
		protected readonly IInputManager inputManager;
		private readonly IMainCameraProvider cameraProvider;
		private readonly ICellBehaviourMapper cellBehaviourMapper;

		protected PointerCellClickHandler() {
			inputManager = Context.Resolve<IInputManager>();
			cameraProvider = Context.Resolve<IMainCameraProvider>();
			cellBehaviourMapper = Context.Resolve<ICellBehaviourMapper>();
		}

		// Sandbox methods 
		protected void OnPointerPress(Vector2 pointerPosition) {
			Ray ray = cameraProvider.GetMainCamera().ScreenPointToRay(pointerPosition);
			if (!Physics.Raycast(ray, out RaycastHit hit))
				return;

			if (cellBehaviourMapper.TryGetCellBehaviour(hit.collider, out LevelCellBehaviour cellBehaviour)) {
				LevelCell cell = cellBehaviour.GetCell();
				if (cell.GetGridElement() is Passenger passenger) {
					
				}
			}
		}
	}

	public class MouseCellClickHandler : PointerCellClickHandler {
		public MouseCellClickHandler() {
			inputManager.MouseInputHandler.MousePressEvent += OnMousePress;
		}

		private void OnMousePress(MouseData mouseData) => OnPointerPress(mouseData.MousePosition);
	}

	public class TouchCellClickHandler : PointerCellClickHandler {
		public TouchCellClickHandler() {
			inputManager.TouchInputHandler.TouchPressEvent += OnTouchPress;
		}

		private void OnTouchPress(TouchData touchData) => OnPointerPress(touchData.PressPosition);
	}
}
