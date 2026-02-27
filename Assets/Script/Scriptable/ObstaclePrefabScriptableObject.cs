using Script.Field.Car;
using Script.Field.Cat;
using Script.Obstacles.Hard;
using UnityEngine;

namespace Script.Scriptable {
	[CreateAssetMenu(fileName = "ObstaclePrefabScriptableObject", menuName = "Scriptable Objects/ObstaclePrefabScriptableObject")]
	public class ObstaclePrefabScriptableObject : ScriptableObject {
		public Cat simple;
		public Car move;
		public ObstaclesHard hard;
	}
}
