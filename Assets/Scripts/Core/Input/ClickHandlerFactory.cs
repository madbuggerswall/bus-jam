using System;
using UnityEngine;

namespace Core.Input {
	public class ClickHandlerFactory : IClickHandlerFactory {
		public IPointerClickHandler Create() {
			return Application.platform switch {
				RuntimePlatform.Android or RuntimePlatform.IPhonePlayer => new TouchCellClickHandler(),
				RuntimePlatform.LinuxEditor or RuntimePlatform.OSXEditor => new MouseCellClickHandler(),
				_ => throw new ArgumentOutOfRangeException()
			};
		}
	}
}