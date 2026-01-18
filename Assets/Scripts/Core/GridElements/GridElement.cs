using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Core.GridElements {
	public class GridElement : MonoBehaviour {
		[SerializeField] private Vector3 pivotLocalPosition;
		[SerializeField] private SquareCoord[] squareCoords;
		
		protected IElementLifecycle lifecycle;

		public virtual void Initialize(IElementLifecycle lifecycle) {
			this.lifecycle = lifecycle;
		}
		
		public IElementLifecycle GetLifecycle() => lifecycle;
		public Vector3 GetPivotWorldPosition() => transform.TransformPoint(pivotLocalPosition);
		public SquareCoord[] GetSquareCoords() => squareCoords;
	}
}
