namespace Core.Persistence {
	public interface IPersistenceManager {
		public bool TryLoad(out PlayerData playerData);
		public void Save(PlayerData playerData);
	}
}