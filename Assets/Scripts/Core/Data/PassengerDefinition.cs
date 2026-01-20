using Core.Passengers;
using UnityEngine;

namespace Core.Data {
	[CreateAssetMenu(menuName = MenuName, fileName = FileName)]
	public class PassengerDefinition : ScriptableObject {
		private const string MenuName = "Definition/Passengers/" + FileName;
		private const string FileName = nameof(PassengerDefinition);
		
		[SerializeField] private Passenger prefab;
		
		public Passenger GetPrefab() => prefab;
	}
}
