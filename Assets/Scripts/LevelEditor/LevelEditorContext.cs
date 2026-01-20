using Core.CameraSystem.Core;
using Core.Data;
using Core.LevelGrids;
using Core.Passengers;
using Core.Waiting.Grids;
using Frolics.Contexts;
using LevelEditor.BusGrids;

namespace LevelEditor {
	public class LevelEditorContext : SubContext<LevelEditorContext> {
		protected override void BindContext() {
			Bind<CameraController>().To<ICameraController>().To<IMainCameraProvider>();

			Bind<LevelGridBehaviourFactory>().To<ILevelGridBehaviourFactory>();
			Bind<LevelCellBehaviourFactory>().To<ILevelCellBehaviourFactory>();
			Bind<WaitingGridBehaviourFactory>().To<IWaitingGridBehaviourFactory>();
			Bind<WaitingCellBehaviourFactory>().To<IWaitingCellBehaviourFactory>();
			Bind<BusGridBehaviourFactory>().To<IBusGridBehaviourFactory>();
			Bind<BusCellBehaviourFactory>().To<IBusCellBehaviourFactory>();

			Bind<GridElementFactory>().To<IGridElementFactory>();
			Bind<EditorBusFactory>().To<IEditorBusFactory>();
			// Bind<BusFactory>().To<IBusFactory>();

			Bind<EditorLevelGridInitializer>().To<ILevelGridBehaviourProvider>().To<ILevelGridProvider>();
			Bind<EditorWaitingGridInitializer>().To<IWaitingGridBehaviourProvider>().To<IWaitingGridProvider>();
			Bind<BusGridInitializer>().To<IBusGridBehaviourProvider>().To<IBusGridProvider>();

			Bind<EditorCellBehaviourMapper>().To<IEditorCellBehaviourMapper>();
			Bind<EditorLevelCellSelector>();
			Bind<CellElementSpawner>();

			Bind<EditorBusCellBehaviourMapper>().To<IEditorBusCellBehaviourMapper>();
			Bind<EditorBusCellSelector>();
			Bind<CellEditorBusSpawner>();
		}

		protected override void OnInitialized() {
			// throw new System.NotImplementedException();
		}
	}
}
