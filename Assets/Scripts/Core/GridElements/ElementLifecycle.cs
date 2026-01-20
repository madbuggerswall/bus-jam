using Core.LevelGrids;
using Frolics.Pooling;

namespace Core.GridElements {
	public class ElementLifecycle : IElementLifecycle {
		private readonly GridElement element;
		private readonly IObjectPool<GridElement> pool;

		public ElementLifecycle(GridElement element, LevelGrid grid, IObjectPool<GridElement> pool) {
			this.element = element;
			this.pool = pool;
		}

		public void Despawn() {
			pool.Despawn(element);
		}
	}
}