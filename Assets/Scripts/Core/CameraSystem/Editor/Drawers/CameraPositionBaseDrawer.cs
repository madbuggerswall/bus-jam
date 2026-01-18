using System;
using Core.CameraSystem.PositionBases;
using Frolics.Utilities.Editor;
using UnityEditor;

namespace Core.CameraSystem.Editor.Drawers {
	[CustomPropertyDrawer(typeof(IPositionBase), true)]
	public class CameraPositionBaseDrawer : ManagedReferenceDrawer {
		protected override Type GetValidType() => typeof(IPositionBase);
		protected override bool IsValidType(Type type) => GetValidType().IsAssignableFrom(type);
	}
}
