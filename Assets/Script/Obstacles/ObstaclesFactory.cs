using Script.Scriptable;
using Object = UnityEngine.Object;

namespace Script.Obstacles {
	public class ObstaclesFactory {
		private readonly ObstaclePrefabScriptableObject _prefabScriptable;

		public ObstaclesFactory(ObstaclePrefabScriptableObject prefabScriptable) {
			_prefabScriptable = prefabScriptable;
		}

		public IObstacles Create(ObstaclesType type) {
			IObstacles obstacles = null;
			
			switch (type) {
				case ObstaclesType.None :
					break;
				case ObstaclesType.Simple :
					obstacles = Object.Instantiate(_prefabScriptable.simple);
					break;
				case ObstaclesType.Move :
					obstacles = Object.Instantiate(_prefabScriptable.move);
					break;
				default:
					return null;
			}

			return obstacles;
		}
	}
}