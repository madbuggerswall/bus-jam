using Core.Data;
using Frolics.Utilities;
using UnityEngine;

namespace Core.Levels {
	public class LevelLoader : MonoBehaviour, IInitializable, ILevelLoader {
		[SerializeField] private LevelDefinition defaultLevelDefinition;

		void IInitializable.Initialize() { }
		
		LevelDTO ILevelLoader.GetLevelData() => defaultLevelDefinition.GetLevelDTO();
	}
}
