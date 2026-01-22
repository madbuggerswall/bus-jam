using Core.LevelGrids;
using Frolics.Contexts;
using Frolics.Signals;
using LevelEditor.UI;
using UnityEngine;

namespace LevelEditor.Tools {
	public class SelectedLevelCellIndicator : MonoBehaviour {
		// Services
		private ISignalBus signalBus;
		private ILevelGridProvider levelGridProvider;

		private void Start() {
			signalBus = Context.Resolve<ISignalBus>();
			levelGridProvider = Context.Resolve<ILevelGridProvider>();

			signalBus.SubscribeTo<SelectedLevelCellChangeSignal>(OnSelectedLevelCellChange);
			gameObject.SetActive(false);
		}

		private void OnSelectedLevelCellChange(SelectedLevelCellChangeSignal signal) {
			if (signal.Cell == null) {
				gameObject.SetActive(false);
			} else {
				gameObject.SetActive(true);
				Vector3 position = levelGridProvider.GetGrid().GetWorldPosition(signal.Cell);
				transform.position = position;
			}
		}
	}
}
