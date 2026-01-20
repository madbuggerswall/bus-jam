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

namespace Core.Input {
	public class CellElementSelector : MonoBehaviour, IInitializable {
		[SerializeField] private Passenger passenger;
		[SerializeField] private ReservedPassenger reservedPassenger;

		[SerializeField] private KeyColorMapDefinition colorMapDefinition;
		[SerializeField] private KeyPrefabMapDefinition prefabMapDefinition;

		private Dictionary<Key, ColorDefinition> colorMap;
		private Dictionary<Key, GridElement> prefabMap;

		// Services
		private IInputManager inputManager;
		private IGridElementFactory gridElementFactory;
		private ILevelGridProvider levelGridProvider;
		private EditorCellSelector cellSelector;

		void IInitializable.Initialize() {
			inputManager = Context.Resolve<IInputManager>();
			inputManager.KeyboardInputHandler.KeyPressEvent += OnKeyPress;

			colorMap = new Dictionary<Key, ColorDefinition>();
			KeyColorMapping[] colorMappings = colorMapDefinition.GetKeyColorMappings();
			for (int i = 0; i < colorMappings.Length; i++)
				colorMap.Add(colorMappings[i].GetKey(), colorMappings[i].GetColorDefinition());

			prefabMap = new Dictionary<Key, GridElement>();
			KeyPrefabMapping[] prefabMappings = prefabMapDefinition.GetKeyPrefabMappings();
			for (int i = 0; i < prefabMappings.Length; i++)
				prefabMap.Add(prefabMappings[i].GetKey(), prefabMappings[i].GetPrefab());
		}

		private void OnKeyPress(KeyData keyData) {
			if (prefabMap.TryGetValue(keyData.KeyControl.keyCode, out GridElement prefab)) {
				SpawnElement(prefab);
			}

			if (colorMap.TryGetValue(keyData.KeyControl.keyCode, out ColorDefinition colorDefinition)) {
				ColorElement(colorDefinition);
			}
		}

		private void SpawnElement(GridElement prefab) {
			LevelCell selectedCell = cellSelector.GetSelectedCell();
			if (selectedCell == null)
				return;

			gridElementFactory.Create(prefab, levelGridProvider.GetGrid(), selectedCell);
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
