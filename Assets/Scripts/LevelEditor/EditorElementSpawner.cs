using System.Collections.Generic;
using Core.Data;
using Core.GridElements;
using Core.LevelGrids;
using Core.Passengers;
using Frolics.Contexts;
using Frolics.Input;
using Frolics.Input.Standalone;
using Frolics.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LevelEditor {
	public class EditorElementSpawner : MonoBehaviour, IInitializable {
		[SerializeField] private KeyColorMapDefinition colorMapDefinition;
		[SerializeField] private KeyPrefabMapDefinition prefabMapDefinition;
		[SerializeField] private ColorDefinition defaultColorDefinition;

		private Dictionary<Key, ColorDefinition> colorMap;
		private Dictionary<Key, GridElement> prefabMap;

		// Services
		private IInputManager inputManager;
		private IGridElementFactory elementFactory;
		private ILevelGridProvider levelGridProvider;
		private IEditorLevelCellSelector cellSelector;

		void IInitializable.Initialize() {
			inputManager = Context.Resolve<IInputManager>();
			elementFactory = Context.Resolve<IGridElementFactory>();
			levelGridProvider = Context.Resolve<ILevelGridProvider>();
			cellSelector = Context.Resolve<IEditorLevelCellSelector>();

			inputManager.KeyboardInputHandler.KeyPressEvent += OnKeyPress;
			InitializeColorMap();
			InitializePrefabMap();
		}

		private void InitializePrefabMap() {
			prefabMap = new Dictionary<Key, GridElement>();
			KeyPrefabMapping[] prefabMappings = prefabMapDefinition.GetKeyPrefabMappings();
			for (int i = 0; i < prefabMappings.Length; i++)
				prefabMap.Add(prefabMappings[i].GetKey(), prefabMappings[i].GetPrefab());
		}

		private void InitializeColorMap() {
			colorMap = new Dictionary<Key, ColorDefinition>();
			KeyColorMapping[] colorMappings = colorMapDefinition.GetKeyColorMappings();
			for (int i = 0; i < colorMappings.Length; i++)
				colorMap.Add(colorMappings[i].GetKey(), colorMappings[i].GetColorDefinition());
		}

		private void OnKeyPress(KeyData keyData) {
			if (prefabMap.TryGetValue(keyData.KeyControl.keyCode, out GridElement prefab)) {
				SpawnElement(prefab);
			}

			if (colorMap.TryGetValue(keyData.KeyControl.keyCode, out ColorDefinition colorDefinition)) {
				ColorElement(colorDefinition);
			}

			if (keyData.KeyControl.keyCode == Key.Backspace) {
				DeleteElement();
			}
		}

		private void SpawnElement(GridElement prefab) {
			LevelCell selectedCell = cellSelector.GetSelectedCell();
			if (selectedCell == null)
				return;

			if (selectedCell.HasElement())
				return;

			GridElement element = elementFactory.Create(prefab, levelGridProvider.GetGrid(), selectedCell);
			if (element is IColorable colorable)
				colorable.SetColorDefinition(defaultColorDefinition);
		}

		private void DeleteElement() {
			LevelCell selectedCell = cellSelector.GetSelectedCell();
			if (selectedCell == null)
				return;

			if (!selectedCell.HasElement())
				return;

			GridElement element = selectedCell.GetGridElement();
			levelGridProvider.GetGrid().RemoveElement(element);
			elementFactory.Despawn(element);
		}

		private void ColorElement(ColorDefinition colorDefinition) {
			LevelCell selectedCell = cellSelector.GetSelectedCell();
			if (selectedCell == null || !selectedCell.HasElement())
				return;

			if (selectedCell.GetGridElement() is not IColorable colorable)
				return;

			colorable.SetColorDefinition(colorDefinition);
		}
	}
}
