using Core.Levels;
using Frolics.Contexts;
using TMPro;
using UnityEngine;

namespace Core.UI {
	public class LevelPanel : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI levelText;

		// Services
		private ILevelPackManager levelPackManager;

		private void Start() {
			levelPackManager = Context.Resolve<ILevelPackManager>();
			levelText.text = $"Level {levelPackManager.GetCurrentLevelIndex()}";
		}
	}
}
