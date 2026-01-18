using System;
using Core.CameraSystem.PositionContributors;
using Frolics.Utilities.Editor;
using UnityEditor;

namespace Core.CameraSystem.Editor.Drawers {
	[CustomPropertyDrawer(typeof(IPositionContributor), true)]
	public class CameraPositionContributorDrawer : ManagedReferenceDrawer {
		protected override Type GetValidType() => typeof(IPositionContributor);
		protected override bool IsValidType(Type type) => GetValidType().IsAssignableFrom(type);
	}
}
