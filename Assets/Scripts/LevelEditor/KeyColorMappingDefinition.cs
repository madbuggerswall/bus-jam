using System;
using Core.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


[CreateAssetMenu(menuName = MenuName, fileName = FileName)]
public class KeyColorMapDefinition : ScriptableObject {
	private const string FileName = nameof(KeyColorMapDefinition);
	private const string MenuName = "Definition/Editor/" + FileName;

	[SerializeField] private KeyColorMapping[] keyColorMappings;
	public KeyColorMapping[] GetKeyColorMappings() => keyColorMappings;
}

[Serializable]
public struct KeyColorMapping {
	[SerializeField] private Key key;
	[SerializeField] private ColorDefinition colorDefinition;

	public Key GetKey() => key;
	public ColorDefinition GetColorDefinition() => colorDefinition;
}
