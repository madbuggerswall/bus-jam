using System.Collections.Generic;
using Core.Data;
using Core.GridElements;
using Core.Passengers;
using Frolics.Contexts;
using Frolics.Input;
using Frolics.Input.Standalone;
using Frolics.Utilities;
using LevelEditor.BusGrids;
using LevelEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LevelEditor.Tools {
	public class EditorBusSpawner : MonoBehaviour, IInitializable {
		[SerializeField] private KeyColorMapDefinition colorMapDefinition;
		[SerializeField] private KeyPrefabMapDefinition prefabMapDefinition;
		[SerializeField] private ColorDefinition defaultColorDefinition;

		private Dictionary<Key, ColorDefinition> colorMap;
		private Dictionary<Key, GridElement> prefabMap;

		// Services
		private IInputManager inputManager;
		private IEditorBusFactory editorBusFactory;
		private IBusGridProvider busGridProvider;
		private IEditorBusCellSelector cellSelector;

		void IInitializable.Initialize() {
			inputManager = Context.Resolve<IInputManager>();
			editorBusFactory = Context.Resolve<IEditorBusFactory>();
			busGridProvider = Context.Resolve<IBusGridProvider>();
			cellSelector = Context.Resolve<IEditorBusCellSelector>();

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

			if (keyData.KeyControl.keyCode == Key.R) {
				IncrementReservedCapacity();
			}
		}

		private void SpawnElement(GridElement prefab) {
			BusCell selectedCell = cellSelector.GetSelectedCell();
			if (selectedCell == null)
				return;

			if (selectedCell.HasBus())
				return;

			EditorBus bus = editorBusFactory.Create(prefab as EditorBus, busGridProvider.GetGrid(), selectedCell);
			bus.Initialize(defaultColorDefinition);
		}

		private void DeleteElement() {
			BusCell selectedCell = cellSelector.GetSelectedCell();
			if (selectedCell == null)
				return;

			if (!selectedCell.HasBus())
				return;

			EditorBus bus = selectedCell.GetBus();
			busGridProvider.GetGrid().RemoveBus(bus);
			editorBusFactory.Despawn(bus);
		}

		private void IncrementReservedCapacity() {
			BusCell selectedCell = cellSelector.GetSelectedCell();
			if (selectedCell == null)
				return;

			if (!selectedCell.HasBus())
				return;

			EditorBus bus = selectedCell.GetBus();
			bus.SetReservedCapacity((bus.GetReservedCapacity() + 1) % (bus.GetCapacity() + 1));
		}

		private void ColorElement(ColorDefinition colorDefinition) {
			BusCell selectedCell = cellSelector.GetSelectedCell();
			if (selectedCell == null || !selectedCell.HasBus())
				return;

			if (selectedCell.GetBus() is not IColorable colorable)
				return;

			colorable.SetColorDefinition(colorDefinition);
		}
	}
}
