using System;
using Core.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LevelEditor.Tools {
	[Serializable]
	public struct KeyColorMapping {
		[SerializeField] private Key key;
		[SerializeField] private ColorDefinition colorDefinition;

		public Key GetKey() => key;
		public ColorDefinition GetColorDefinition() => colorDefinition;
	}
}