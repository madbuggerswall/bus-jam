using System.Collections.Generic;
using Core.Buses;
using Core.CameraSystem.Core;
using Core.LevelGrids;
using Core.Passengers;
using Core.PathFinding;
using Frolics.Contexts;
using Frolics.Grids.SpatialHelpers;
using Frolics.Input;
using UnityEngine;

namespace Core.Input {
	public abstract class PointerCellClickHandler : IPointerClickHandler {
		// Services
		protected readonly IInputManager inputManager;
		private readonly IMainCameraProvider cameraProvider;
		private readonly ICellBehaviourMapper cellBehaviourMapper;
		
		private readonly IPathFinder pathFinder;
		private readonly BusManager busManager; 
		private readonly ILevelGridProvider levelGridProvider;

		protected PointerCellClickHandler() {
			inputManager = Context.Resolve<IInputManager>();
			cameraProvider = Context.Resolve<IMainCameraProvider>();
			cellBehaviourMapper = Context.Resolve<ICellBehaviourMapper>();
			
			pathFinder = Context.Resolve<IPathFinder>();
			busManager = Context.Resolve<BusManager>();
			levelGridProvider = Context.Resolve<ILevelGridProvider>();
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

			if (!pathFinder.IsTargetReachable(cell.GetCoord())) 
				return;
			
			bool colorMatch = passenger.GetColor() == busManager.GetCurrentBus().GetColor();
			if (!colorMatch) {
				// TODO WaitingArea
				return;
			}

			List<SquareCoord> squareCoords = pathFinder.GetPath(cell.GetCoord());
			passenger.GetController().PlayPathTween(levelGridProvider.GetGrid(), squareCoords);
		}
	}
}
