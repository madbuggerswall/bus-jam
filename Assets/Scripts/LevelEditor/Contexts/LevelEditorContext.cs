using Core;
using Core.CameraSystem.Core;
using Core.Data;
using Core.GridElements;
using Core.LevelGrids;
using Core.Mechanics;
using Core.Passengers;
using Core.Waiting.Grids;
using Frolics.Contexts;
using LevelEditor.BusGrids;
using LevelEditor.SaveSystem;
using LevelEditor.Tools;

namespace LevelEditor.Contexts {
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

			Bind<LevelTimeProvider>().To<ILevelTimeProvider>();
			Bind<EditorLevelGridInitializer>()
				.To<IEditorLevelGridInitializer>()
				.To<ILevelGridBehaviourProvider>()
				.To<ILevelGridProvider>();

			Bind<EditorWaitingGridInitializer>()
				.To<IEditorWaitingGridInitializer>()
				.To<IWaitingGridBehaviourProvider>()
				.To<IWaitingGridProvider>();

			Bind<BusGridInitializer>().To<IBusGridInitializer>().To<IBusGridBehaviourProvider>().To<IBusGridProvider>();

			Bind<EditorCellBehaviourMapper>().To<IEditorCellBehaviourMapper>();
			Bind<EditorLevelCellSelector>().To<IEditorLevelCellSelector>();
			Bind<EditorElementSpawner>();

			Bind<EditorBusCellBehaviourMapper>().To<IEditorBusCellBehaviourMapper>();
			Bind<EditorBusCellSelector>().To<IEditorBusCellSelector>();
			Bind<EditorBusSpawner>();

			Bind<LevelDataSaver>().To<ILevelDataSaver>();
			Bind<LevelDefinitionSaver>().To<ILevelDefinitionSaver>();

			Bind<PassengerDTOSpawner>().To<IPassengerDTOSpawner>();
			Bind<BusDTOSpawner>().To<IBusDTOSpawner>();
			Bind<LevelDefinitionLoader>().To<ILevelDefinitionLoader>();
			Bind<EditorLevelLoader>().To<IEditorLevelLoader>();
		}

		protected override void OnInitialized() {
			Resolve<IEditorLevelGridInitializer>().CreateGrid();
			Resolve<IEditorWaitingGridInitializer>().CreateGrid();
			Resolve<IBusGridInitializer>().CreateGrid();
		}
	}
}
