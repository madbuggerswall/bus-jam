using UnityEngine;

namespace Core.Data {
	[CreateAssetMenu(menuName = MenuName, fileName = FileName)]
	public class ColorDefinition : ScriptableObject {
		private const string MenuName = "Definition/Colors/" + FileName;
		private const string FileName = nameof(ColorDefinition);
		
		[SerializeField] private Material material;
		
		public Material GetMaterial() => material;
	}
}
