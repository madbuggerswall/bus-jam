using System;
using Core.GridElements;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = MenuName, fileName = FileName)]
public class KeyPrefabMapDefinition : ScriptableObject {
	private const string FileName = nameof(KeyPrefabMapDefinition);
	private const string MenuName = "Definition/Editor/" + FileName;

	[SerializeField] private KeyPrefabMapping[] keyPrefabMappings;
	public KeyPrefabMapping[] GetKeyPrefabMappings() => keyPrefabMappings;
}

[Serializable]
public struct KeyPrefabMapping {
	[SerializeField] private Key key;
	[SerializeField] private GridElement gridElement;

	public Key GetKey() => key;
	public GridElement GetPrefab() => gridElement;
}
