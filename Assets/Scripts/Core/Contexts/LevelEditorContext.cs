using Core.Buses;
using Core.CameraSystem.Core;
using Core.LevelGrids;
using Core.Passengers;
using Core.Waiting.Grids;
using Frolics.Contexts;

namespace Core.Contexts {
	public class LevelEditorContext : SubContext<LevelEditorContext> {
		protected override void BindContext() {
			Bind<CameraController>().To<ICameraController>().To<IMainCameraProvider>();
			
			Bind<LevelGridBehaviourFactory>().To<ILevelGridBehaviourFactory>();
			Bind<LevelCellBehaviourFactory>().To<ILevelCellBehaviourFactory>();
			Bind<WaitingGridBehaviourFactory>().To<IWaitingGridBehaviourFactory>();
			Bind<WaitingCellBehaviourFactory>().To<IWaitingCellBehaviourFactory>();
			Bind<GridElementFactory>().To<IGridElementFactory>();
			Bind<BusFactory>().To<IBusFactory>();
		}

		protected override void OnInitialized() {
			throw new System.NotImplementedException();
		}
	}
}
