using System;
using System.Collections.Generic;
using R3;
using Script.Scriptable;
using UnityEngine;

namespace Script.Obstacles {
	/// <summary>
	/// 生成した障害物の管理
	/// </summary>
	public class ObstaclesPool : MonoBehaviour {
		[SerializeField] private ObstaclePrefabScriptableObject prefabScriptable;
		[SerializeField] private int maxCount;
		[SerializeField] private ObstaclesSpawner spawner;
		private readonly IDictionary<ObstaclesType, Queue<IObstacles>> _poolDictionary = new Dictionary<ObstaclesType, Queue<IObstacles>>();
		private ObstaclesFactory _factory;

		public void Awake() {
			_factory = new ObstaclesFactory(prefabScriptable);

			// 生成
			foreach (ObstaclesType type in Enum.GetValues(typeof(ObstaclesType))) _poolDictionary[type] = CreateObstaclesPool(type);
		}

		/// <summary>
		/// プールから借りる
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public IObstacles Rent(ObstaclesType type) {
			// 例外：獲得できなかったら
			if (!_poolDictionary.TryGetValue(type, out Queue<IObstacles> pool)) {
				pool = new Queue<IObstacles>();
				_poolDictionary[type] = pool;
			}
			
			// キューから取り出す：数が足りれば
			IObstacles obstacles = pool.Count > 0 ? pool.Dequeue() : _factory.Create(type);
			
			return obstacles;
		}

		/// <summary>
		/// プールに戻す
		/// </summary>
		/// <param name="obstacles"></param>
		private void Return(IObstacles obstacles) {
			obstacles.Initialize(parent: transform);
			_poolDictionary[obstacles.ObstaclesType].Enqueue(obstacles);
		}
		
		/// <summary>
		/// プール生成
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		private Queue<IObstacles> CreateObstaclesPool(ObstaclesType type) {
			Queue<IObstacles> queue = new Queue<IObstacles>();
			
			for (int i = 0; i < maxCount; i++) {
				// 生成
				IObstacles obstacles = _factory.Create(type);

				// continue：生成失敗してたら
				if (obstacles == null) continue;

				// 非表示
				obstacles.Initialize(parent: transform);

				// 購読：プールに戻す処理
				obstacles.ReturnToPoolObservable.Subscribe(_ => Return(obstacles)).RegisterTo(destroyCancellationToken);

				// キューに追加
				queue.Enqueue(obstacles);
			}
			
			return queue;
		}
	}
}