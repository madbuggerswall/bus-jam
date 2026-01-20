using Core.Data;
using UnityEngine;

namespace Core.Passengers.Types {
	public class RopePassenger : Passenger {
		[SerializeField] private Transform[] ropeTransforms;

		private const int DefaultRopeDurability = 3;
		private int ropeDurability;

		public override void Initialize(ColorDefinition colorDefinition) {
			base.Initialize(colorDefinition);
			ropeDurability = DefaultRopeDurability;

			for (int i = 0; i < ropeTransforms.Length; i++)
				ropeTransforms[i].gameObject.SetActive(true);
		}

		public override bool CanMove() {
			return ropeDurability <= 0;
		}

		public override void OnNeighborMove() {
			ropeDurability--;
			UpdateRope();
		}

		public override void OnAnyMove() { }

		private void UpdateRope() {
			int index = DefaultRopeDurability - ropeDurability - 1;
			if (index < 0 || index >= ropeTransforms.Length)
				return;

			ropeTransforms[index].gameObject.SetActive(false);
		}
	}
}
