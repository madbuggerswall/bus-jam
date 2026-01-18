using Frolics.Pooling;

namespace Core.GridElements {
	public interface IElementLifecycle {
		public void RemoveFromGrid();
		public void Despawn();
	}

	public class ElementLifecycle : IElementLifecycle {
		private readonly GridElement element;
		private readonly LevelGrid grid;
		private readonly IObjectPool<GridElement> pool;

		public ElementLifecycle(GridElement element, LevelGrid grid, IObjectPool<GridElement> pool) {
			this.element = element;
			this.grid = grid;
			this.pool = pool;
		}
		public void RemoveFromGrid() {
			
		}
		public void Despawn() {
			throw new System.NotImplementedException();
		}
	}
}
