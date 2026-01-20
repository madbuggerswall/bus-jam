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
using UnityEngine.InputSystem.Controls;

namespace Core.Input {
	public class CellElementSelector : MonoBehaviour, IInitializable {
		[SerializeField] private Passenger passenger;
		[SerializeField] private ReservedPassenger reservedPassenger;

		private Dictionary<KeyControl, PassengerColor> colorMap;
		private Dictionary<KeyControl, GridElement> passengerMap;

		private GridElement currentElement;

		// Services
		private IInputManager inputManager;
		private IGridElementFactory gridElementFactory;
		private ILevelGridProvider levelGridProvider;
		private EditorCellSelector cellSelector;

		void IInitializable.Initialize() {
			inputManager = Context.Resolve<IInputManager>();
			inputManager.KeyboardInputHandler.KeyPressEvent += OnKeyPress;

			colorMap = new Dictionary<KeyControl, PassengerColor> {
				{ Keyboard.current.digit1Key, PassengerColor.Blue },
				{ Keyboard.current.digit2Key, PassengerColor.Brown },
				{ Keyboard.current.digit3Key, PassengerColor.Cyan },
				{ Keyboard.current.digit4Key, PassengerColor.Green },
				{ Keyboard.current.digit5Key, PassengerColor.Orange },
				{ Keyboard.current.digit6Key, PassengerColor.Pink },
				{ Keyboard.current.digit7Key, PassengerColor.Purple },
				{ Keyboard.current.digit8Key, PassengerColor.Red },
				{ Keyboard.current.digit9Key, PassengerColor.White },
				{ Keyboard.current.digit0Key, PassengerColor.Yellow }
			};

			passengerMap = new Dictionary<KeyControl, GridElement> {
				{ Keyboard.current.zKey, passenger },
				{ Keyboard.current.xKey, reservedPassenger },
			};
		}

		private void OnKeyPress(KeyData keyData) {
			if (passengerMap.TryGetValue(keyData.KeyControl, out GridElement elementPrefab)) { }

			if (colorMap.TryGetValue(keyData.KeyControl, out PassengerColor passengerColor)) { }
		}

		private void SpawnElement(GridElement prefab) {
			LevelCell selectedCell = cellSelector.GetSelectedCell();
			if (selectedCell == null)
				return;

			GridElement element = gridElementFactory.Create(prefab, levelGridProvider.GetGrid(), selectedCell);
			currentElement = element;
		}
		
		private void ColorElement(){}

		public GridElement GetCurrentElement() => currentElement;
	}
}
