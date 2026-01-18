using Frolics.Contexts;
using Frolics.Input;
using Frolics.Signals;
using UnityEngine;

namespace Core.Contexts {
	public class MainContext : ProjectContext {
		protected override void BindContext() {
			Bind<SignalBus>().To<ISignalBus>();
			Bind<InputManager>().To<IInputManager>();
		}

		protected override void OnInitialized() {
			Debug.Log("MainContext: OnInitialized");
		}
	}
}
