using Core.Buses;
using Core.CameraSystem.Core;
using Core.Data;
using Core.LevelGrids;
using Core.Passengers;
using Core.Waiting.Grids;
using Frolics.Contexts;

namespace LevelEditor {
	public class LevelEditorContext : SubContext<LevelEditorContext> {
		protected override void BindContext() {
			Bind<CameraController>().To<ICameraController>().To<IMainCameraProvider>();
			Bind<PassengerColorManager>().To<IPassengerColorManager>();

			Bind<LevelGridBehaviourFactory>().To<ILevelGridBehaviourFactory>();
			Bind<LevelCellBehaviourFactory>().To<ILevelCellBehaviourFactory>();
			Bind<WaitingGridBehaviourFactory>().To<IWaitingGridBehaviourFactory>();
			Bind<WaitingCellBehaviourFactory>().To<IWaitingCellBehaviourFactory>();
			Bind<GridElementFactory>().To<IGridElementFactory>();
			Bind<BusFactory>().To<IBusFactory>();

			Bind<EditorLevelGridInitializer>().To<ILevelGridBehaviourProvider>().To<ILevelGridProvider>();
			Bind<EditorWaitingGridInitializer>().To<IWaitingGridBehaviourProvider>().To<IWaitingGridProvider>();

			Bind<EditorCellBehaviourMapper>().To<IEditorCellBehaviourMapper>();
			Bind<EditorCellSelector>();
			Bind<CellElementSelector>();
		}

		protected override void OnInitialized() {
			// throw new System.NotImplementedException();
		}
	}
}
