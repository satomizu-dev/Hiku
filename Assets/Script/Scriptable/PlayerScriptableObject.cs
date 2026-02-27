using System.Collections.Generic;
using UnityEngine;

namespace Script.Scriptable {
	[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "Scriptable Objects/PlayerScriptableObject")]
	public class PlayerScriptableObject : ScriptableObject {
		public List<int> borderLevelList;
	}
}
