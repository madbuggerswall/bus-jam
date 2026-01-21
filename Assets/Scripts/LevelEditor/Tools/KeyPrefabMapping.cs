using System;
using Core.GridElements;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LevelEditor.Tools {
	[Serializable]
	public struct KeyPrefabMapping {
		[SerializeField] private Key key;
		[SerializeField] private GridElement gridElement;

		public Key GetKey() => key;
		public GridElement GetPrefab() => gridElement;
	}
}