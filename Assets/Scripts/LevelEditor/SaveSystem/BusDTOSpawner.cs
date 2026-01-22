using Core.Data;
using Frolics.Contexts;
using Frolics.Utilities;
using LevelEditor.BusGrids;
using LevelEditor.EditorBuses;
using UnityEngine;

namespace LevelEditor.SaveSystem {
	public class BusDTOSpawner : MonoBehaviour, IInitializable, IBusDTOSpawner {
		[SerializeField] private EditorBus prefab;

		private IEditorBusFactory editorBusFactory;
		private IBusGridProvider busGridProvider;

		void IInitializable.Initialize() {
			editorBusFactory = Context.Resolve<IEditorBusFactory>();
			busGridProvider = Context.Resolve<IBusGridProvider>();
		}

		void IBusDTOSpawner.SpawnBus(BusDTO busDTO, BusCell cell) {
			EditorBus bus = editorBusFactory.Create(prefab, busGridProvider.GetGrid(), cell);
			bus.Initialize(busDTO);
		}
	}
}
