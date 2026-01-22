using Core.Levels.Signals;
using Frolics.Contexts;
using Frolics.Signals;
using UnityEngine;

namespace Core.UI {
	public class LevelStatePanelController : MonoBehaviour {
		[SerializeField] private LevelStatePanel levelSuccessPanel;
		[SerializeField] private LevelStatePanel levelFailPanel;

		// Services
		private ISignalBus signalBus;

		private void Start() {
			signalBus = Context.Resolve<ISignalBus>();
			signalBus.SubscribeTo<LevelSuccessSignal>(OnLevelSuccess);
			signalBus.SubscribeTo<LevelFailSignal>(OnLevelFail);
		}

		private void OnLevelSuccess(LevelSuccessSignal signal) => levelSuccessPanel.gameObject.SetActive(true);
		private void OnLevelFail(LevelFailSignal signal) => levelFailPanel.gameObject.SetActive(true);
	}
}
