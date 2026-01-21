using System.Globalization;
using Core.Mechanics;
using Frolics.Contexts;
using Frolics.Signals;
using LevelEditor;
using LevelEditor.BusGrids;
using LevelEditor.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorPanel : MonoBehaviour {
	[Header("LevelGrid")]
	[SerializeField] private TMP_InputField levelGridSizeX;
	[SerializeField] private TMP_InputField levelGridSizeY;
	[SerializeField] private Button levelGridApplyButton;

	[Header("WaitingGrid")]
	[SerializeField] private TMP_InputField waitingGridSizeX;
	[SerializeField] private TMP_InputField waitingGridSizeY;
	[SerializeField] private Button waitingGridApplyButton;

	[Header("BusGrid")]
	[SerializeField] private TMP_InputField busGridSizeX;
	[SerializeField] private TMP_InputField busGridSizeY;
	[SerializeField] private Button busGridApplyButton;

	[Header("TimerGrid")]
	[SerializeField] private TMP_InputField levelTime;
	[SerializeField] private Button levelTimerApplyButton;

	[Header("SaveLoad")]
	[SerializeField] private Button saveButton;
	[SerializeField] private Button loadButton;


	// Services
	private ISignalBus signalBus;
	private IEditorLevelGridInitializer levelGridInitializer;
	private IEditorWaitingGridInitializer waitingGridInitializer;
	private IBusGridInitializer busGridInitializer;
	private ILevelTimeProvider levelTimeProvider;
	private ILevelDefinitionSaver levelDefinitionSaver;
	private IEditorLevelLoader editorLevelLoader;


	private void Start() {
		signalBus = Context.Resolve<ISignalBus>();
		levelGridInitializer = Context.Resolve<IEditorLevelGridInitializer>();
		waitingGridInitializer = Context.Resolve<IEditorWaitingGridInitializer>();
		busGridInitializer = Context.Resolve<IBusGridInitializer>();
		levelTimeProvider = Context.Resolve<ILevelTimeProvider>();

		levelDefinitionSaver = Context.Resolve<ILevelDefinitionSaver>();
		editorLevelLoader = Context.Resolve<IEditorLevelLoader>();

		levelGridApplyButton.onClick.AddListener(OnLevelGridSizeApplyClick);
		waitingGridApplyButton.onClick.AddListener(OnWaitingGridSizeApplyClick);
		busGridApplyButton.onClick.AddListener(OnBusGridSizeApplyClick);
		levelTimerApplyButton.onClick.AddListener(OnLevelTimerApplyClick);

		saveButton.onClick.AddListener(OnSaveClick);
		loadButton.onClick.AddListener(OnLoadClick);
		
		signalBus.SubscribeTo<EditorLevelLoadedSignal>(OnEditorLevelLoaded);
	}

	private void OnEditorLevelLoaded(EditorLevelLoadedSignal signal) {
		float duration = signal.LevelDTO.GetLevelTime();
		Vector2Int waitingGridSize = signal.LevelDTO.GetWaitingGridSize();
		Vector2Int busGridSize = signal.LevelDTO.GetBusGridSize();
		Vector2Int levelGridSize = signal.LevelDTO.GetLevelGridSize();
		
		levelGridSizeX.text = levelGridSize.x.ToString();
		levelGridSizeY.text = levelGridSize.y.ToString();
		waitingGridSizeX.text = waitingGridSize.x.ToString();
		waitingGridSizeY.text = waitingGridSize.y.ToString();
		busGridSizeX.text = busGridSize.x.ToString();
		busGridSizeY.text = busGridSize.y.ToString();
		levelTime.text = Mathf.RoundToInt(duration).ToString();
	}

	private void OnLevelGridSizeApplyClick() {
		if (!int.TryParse(levelGridSizeX.text, out int gridSizeX))
			return;

		if (!int.TryParse(levelGridSizeY.text, out int gridSizeY))
			return;

		levelGridInitializer.SetGridSize(new Vector2Int(gridSizeX, gridSizeY));
		levelGridInitializer.CreateGrid();
	}

	private void OnWaitingGridSizeApplyClick() {
		if (!int.TryParse(waitingGridSizeX.text, out int gridSizeX))
			return;

		if (!int.TryParse(waitingGridSizeY.text, out int gridSizeY))
			return;

		waitingGridInitializer.SetGridSize(new Vector2Int(gridSizeX, gridSizeY));
		waitingGridInitializer.CreateGrid();
	}

	private void OnBusGridSizeApplyClick() {
		if (!int.TryParse(busGridSizeX.text, out int gridSizeX))
			return;

		if (!int.TryParse(busGridSizeY.text, out int gridSizeY))
			return;

		busGridInitializer.SetGridSize(new Vector2Int(gridSizeX, gridSizeY));
		busGridInitializer.CreateGrid();
	}

	private void OnLevelTimerApplyClick() {
		if (!int.TryParse(levelTime.text, out int duration))
			return;

		levelTimeProvider.SetLevelTime(duration);
	}

	private void OnSaveClick() {
		levelDefinitionSaver.SaveLevelDefinition();
	}

	private void OnLoadClick() {
		editorLevelLoader.LoadLevel();
	}
}
