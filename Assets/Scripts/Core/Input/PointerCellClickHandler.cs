using Core.Buses;
using Core.CameraSystem.Core;
using Core.LevelGrids;
using Core.Passengers;
using Core.PathFinding;
using Core.Waiting.Grids;
using Frolics.Contexts;
using Frolics.Input;
using Frolics.Signals;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Input {
	public abstract class PointerCellClickHandler : IPointerClickHandler {
		// Services
		protected readonly IInputManager inputManager;
		private readonly IMainCameraProvider cameraProvider;
		private readonly ICellBehaviourMapper cellBehaviourMapper;
		private readonly RuleManager ruleManager;

		protected PointerCellClickHandler() {
			inputManager = Context.Resolve<IInputManager>();
			cameraProvider = Context.Resolve<IMainCameraProvider>();
			cellBehaviourMapper = Context.Resolve<ICellBehaviourMapper>();
			ruleManager = Context.Resolve<RuleManager>();
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

			ruleManager.OnPassengerSelected(passenger, cell);
		}
	}

	// TODO Rename
	public class RuleManager : IInitializable {
		// Services
		private ISignalBus signalBus;
		private IPathFinder pathFinder;
		private IBusManager busManager;
		private ILevelGridProvider gridProvider;
		private IWaitingGridProvider waitingGridProvider;
		private IPassengerController passengerController;

		void IInitializable.Initialize() {
			signalBus = Context.Resolve<ISignalBus>();
			pathFinder = Context.Resolve<IPathFinder>();
			busManager = Context.Resolve<IBusManager>();
			gridProvider = Context.Resolve<ILevelGridProvider>();
			waitingGridProvider = Context.Resolve<IWaitingGridProvider>();
			passengerController = Context.Resolve<IPassengerController>();
		}

		public void OnPassengerSelected(Passenger passenger, LevelCell cell) {
			if (!pathFinder.IsTargetReachable(cell.GetCoord())) {
				passenger.GetTweenHelper().PlayUnreachableTween();
				return;
			}

			Bus currentBus = busManager.GetCurrentBus();
			WaitingGrid waitingGrid = waitingGridProvider.GetGrid();
			LevelGrid levelGrid = gridProvider.GetGrid();

			if (currentBus.CanPassengerRide(passenger)) {
				currentBus.HavePassenger(passenger);
				levelGrid.RemoveElement(passenger);
				pathFinder.OnGridModified();
				signalBus.Fire(new PassengerBoardSignal(currentBus, passenger, cell));
				// passengerController.PlayGridToBus(passenger, cell);

				if (currentBus.IsFull()) {
					busManager.OnBusFill();
					CheckWaitingArea();
				}
			} else {
				if (!waitingGrid.TryPlacePassenger(passenger, out WaitingCell waitingCell))
					return;

				levelGrid.RemoveElement(passenger);
				pathFinder.OnGridModified();
				signalBus.Fire(new PassengerWaitSignal(passenger, cell, waitingCell));
				// passengerController.PlayGridToWaiting(passenger, cell, waitingCell);

				if (!waitingGrid.HasEmptyCells()) {
					// TODO GameOver
				}
			}
		}

		private void CheckWaitingArea() {
			Bus bus = busManager.GetCurrentBus();
			WaitingGrid grid = waitingGridProvider.GetGrid();
			WaitingCell[] cells = grid.GetCells();
			for (int i = 0; i < cells.Length; i++) {
				WaitingCell cell = cells[i];
				if (!cell.HasPassenger())
					continue;

				Passenger passenger = cell.GetPassenger();
				if (!bus.CanPassengerRide(passenger))
					continue;

				bus.HavePassenger(passenger);
				grid.RemovePassenger(passenger);
				signalBus.Fire(new WaitingPassengerBoardSignal(bus, passenger));
				// passengerController.PlayWaitingToBus(passenger);
			}
		}
	}
}
