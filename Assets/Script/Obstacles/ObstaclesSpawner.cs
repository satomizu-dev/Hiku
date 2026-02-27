using System;
using UnityEngine;

namespace Script.Obstacles {
	/// <summary>
	/// 障害物のスポーン管理
	/// </summary>
	public class ObstaclesSpawner : MonoBehaviour{
		[SerializeField] private ObstaclesPool obstaclesPool;
		[SerializeField] private int spawnCount;
		
		// これまでに生成した数
		private int _spawnCount;
		
		public void Start() {
			UnityEngine.Random.InitState(DateTime.Now.Millisecond);
			for (int i = 0; i < spawnCount; i++) Spawn();
		}

		/// <summary>
		/// スポーン
		/// </summary>
		public void Spawn() {
			ObstaclesType type = (ObstaclesType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(ObstaclesType)).Length);
			IObstacles obstacles = obstaclesPool.Rent(type);
			if(obstacles == null) return;
			obstacles.Initialize(new Vector3(0, 0, _spawnCount * 2), transform, true, true);
			_spawnCount++;
		}
	}
}