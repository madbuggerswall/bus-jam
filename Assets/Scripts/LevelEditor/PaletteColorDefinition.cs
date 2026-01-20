using System;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(menuName = MenuName, fileName = FileName)]
public class PaletteColorDefinition : ScriptableObject {
	private const string FileName = nameof(PaletteColorDefinition);
	private const string MenuName = "Definition/Editor/" + FileName;

	[SerializeField] private KeyColorMapping[] keyColorMappings;
	public KeyColorMapping[] GetKeyColorMappings() => keyColorMappings;
}

[Serializable]
public struct KeyColorMapping {
	[SerializeField] private Key key;
	[SerializeField] private Color color;

	public Key GetKey() => key;
	public Color GetColor() => color;
}
