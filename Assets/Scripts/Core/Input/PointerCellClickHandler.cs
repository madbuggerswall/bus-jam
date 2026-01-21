using Core.CameraSystem.Core;
using Core.LevelGrids;
using Core.Mechanics;
using Core.Passengers;
using Frolics.Contexts;
using Frolics.Input;
using UnityEngine;

namespace Core.Input {
	public abstract class PointerCellClickHandler : IPointerCellClickHandler {
		// Services
		protected readonly IInputManager inputManager;
		private readonly IMainCameraProvider cameraProvider;
		private readonly ICellBehaviourMapper cellBehaviourMapper;
		private readonly IRuleService ruleService;

		protected PointerCellClickHandler() {
			inputManager = Context.Resolve<IInputManager>();
			cameraProvider = Context.Resolve<IMainCameraProvider>();
			cellBehaviourMapper = Context.Resolve<ICellBehaviourMapper>();
			ruleService = Context.Resolve<IRuleService>();
		}

		// Sandbox methods 
		protected void OnPointerPress(Vector2 pointerPosition) {
			Ray ray = cameraProvider.GetMainCamera().ScreenPointToRay(pointerPosition);
			if (!Physics.Raycast(ray, out RaycastHit hit))
				return;

			if (!cellBehaviourMapper.TryGetCellBehaviour(hit.collider, out LevelCellBehaviour cellBehaviour))
				return;

			LevelCell cell = cellBehaviour.GetCell();
			if (cell.GetGridElement() is not Passenger passenger)
				return;

			ruleService.OnPassengerSelected(passenger, cell);
		}
	}
}
