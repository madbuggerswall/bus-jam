using Frolics.Contexts;
using Frolics.Signals;
using LevelEditor.BusGrids;
using LevelEditor.UI.Signals;
using UnityEngine;

namespace LevelEditor.Tools {
	public class SelectedBusCellIndicator : MonoBehaviour {
		// Services
		private ISignalBus signalBus;
		private IBusGridProvider busGridProvider;

		private void Start() {
			signalBus = Context.Resolve<ISignalBus>();
			busGridProvider = Context.Resolve<IBusGridProvider>();

			signalBus.SubscribeTo<SelectedBusCellChangeSignal>(OnSelectedBusCellChange);
			gameObject.SetActive(false);
		}

		private void OnSelectedBusCellChange(SelectedBusCellChangeSignal signal) {
			if (signal.Cell == null) {
				gameObject.SetActive(false);
			} else {
				gameObject.SetActive(true);
				Vector3 position = busGridProvider.GetGrid().GetWorldPosition(signal.Cell);
				transform.position = position;
			}
		}
	}
}
