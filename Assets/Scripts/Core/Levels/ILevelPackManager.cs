using Core.Data;

namespace Core.Levels {
	public interface ILevelPackManager {
		public LevelDefinition GetLastPlayedLevel();
		public int GetLevelCount();
		public int GetCurrentLevelIndex();
	}
}