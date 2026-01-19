using System.Collections.Generic;
using Core.LevelGrids;
using Core.PathFinding;
using Core.Waiting.Grids;
using Frolics.Contexts;
using Frolics.Grids.SpatialHelpers;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Passengers {
	public class PassengerController : MonoBehaviour, IInitializable, IPassengerController {
		[SerializeField] private Transform busMountPoint;

		// Services
		private ILevelGridProvider gridProvider;
		private IWaitingGridProvider waitingGridProvider;
		private IPathFinder pathFinder;

		void IInitializable.Initialize() {
			gridProvider = Context.Resolve<ILevelGridProvider>();
			waitingGridProvider = Context.Resolve<IWaitingGridProvider>();
			pathFinder = Context.Resolve<IPathFinder>();
		}

		void IPassengerController.PlayWaitingToBus(Passenger passenger) {
			List<Vector3> positions = new();
			positions.Add(passenger.transform.position);
			positions.Add(busMountPoint.position);
			passenger.GetTweenHelper().PlayPathTween(positions);
		}

		void IPassengerController.PlayGridToBus(Passenger passenger, LevelCell cell) {
			List<SquareCoord> coords = pathFinder.GetPath(cell.GetCoord());
			List<Vector3> positions = new();
			LevelGrid grid = gridProvider.GetGrid();

			for (int i = 0; i < coords.Count; i++)
				positions.Add(grid.ToWorldPosition(coords[i]));

			positions.Add(busMountPoint.position);
			passenger.GetTweenHelper().PlayPathTween(positions);
		}

		void IPassengerController.PlayGridToWaiting(Passenger passenger, LevelCell cell, WaitingCell waitingCell) {
			List<SquareCoord> coords = pathFinder.GetPath(cell.GetCoord());
			List<Vector3> positions = new();
			LevelGrid grid = gridProvider.GetGrid();
			WaitingGrid waitingGrid = waitingGridProvider.GetGrid();

			for (int i = 0; i < coords.Count; i++)
				positions.Add(grid.ToWorldPosition(coords[i]));

			positions.Add(waitingGrid.GetWorldPosition(waitingCell));
			passenger.GetTweenHelper().PlayPathTween(positions);
		}
	}
}
