using System.Text;
using Core.Mechanics;
using Frolics.Contexts;
using TMPro;
using UnityEngine;

namespace Core.UI {
	public class TimerPanel : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI timerText;
		[SerializeField] private float updateInterval = 0.5f;

		private readonly StringBuilder stringBuilder = new StringBuilder(16);
		private float accumulator;

		// Services
		private ITimerManager timerManager;

		private void Start() {
			timerManager = Context.Resolve<ITimerManager>();
			UpdateTimerText();
		}

		private void Update() {
			Tick();
		}

		private void Tick() {
			accumulator += Time.deltaTime;
			if (!(accumulator >= updateInterval))
				return;

			// consume intervals (handles frame spikes)
			accumulator -= updateInterval;
			UpdateTimerText();
		}

		private void UpdateTimerText() {
			float remainingSeconds = timerManager.GetRemainingTime();
			int minutes = (int) (remainingSeconds / 60);
			int seconds = (int) (remainingSeconds % 60);

			stringBuilder.Clear();
			stringBuilder.Append(minutes.ToString("D2"));
			stringBuilder.Append(':');
			stringBuilder.Append(seconds.ToString("D2"));
			timerText.text = stringBuilder.ToString();
		}
	}
}
