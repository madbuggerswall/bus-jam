using System;
using Core.CameraSystem.PositionModifiers;
using Frolics.Utilities.Editor;
using UnityEditor;

namespace Core.CameraSystem.Editor.Drawers {
	[CustomPropertyDrawer(typeof(IPositionModifier), true)]
	public class CameraPositionModifierDrawer : ManagedReferenceDrawer {
		protected override Type GetValidType() => typeof(IPositionModifier);
		protected override bool IsValidType(Type type) => GetValidType().IsAssignableFrom(type);
	}
}
