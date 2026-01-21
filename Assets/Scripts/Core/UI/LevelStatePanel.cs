using Core.Levels;
using Core.Persistence;
using Frolics.Contexts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.UI {
	public class LevelStatePanel : MonoBehaviour {
		[SerializeField] private Button nextLevelButton;
		[SerializeField] private Button restartButton;

		private IPersistenceManager persistenceManager;
		private ILevelPackManager levelPackManager;

		private void Start() {
			persistenceManager = Context.Resolve<IPersistenceManager>();
			levelPackManager = Context.Resolve<ILevelPackManager>();

			if (nextLevelButton != null)
				nextLevelButton.onClick.AddListener(OnNextButtonClick);

			if (restartButton != null)
				restartButton.onClick.AddListener(OnRestartButtonClick);
		}

		private void OnNextButtonClick() {
			int currentIndex = levelPackManager.GetCurrentLevelIndex();
			int index = Mathf.Clamp(currentIndex + 1, 0, levelPackManager.GetLevelCount() - 1);
			PlayerData playerData = new(index);
			persistenceManager.Save(playerData);

			Scene activeScene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(activeScene.buildIndex);
		}


		private void OnRestartButtonClick() {
			Scene activeScene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(activeScene.buildIndex);
		}
	}
}
