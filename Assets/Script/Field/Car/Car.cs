using Script.Obstacles;
using Script.Obstacles.AttributeInterface;
using UnityEngine;

namespace Script.Field.Car {
	public class Car : Obstacles.Obstacles , IObstacles {
		private IObstaclesMoveable _moveable;
		
		public void Start() {
			ObstaclesType = ObstaclesType.Move;
		}
		
		public void Initialize(Vector3 position = default, Transform parent = null, bool active = false, bool initializePresenter = false) {
			transform.position = position;
			transform.parent = parent;
			gameObject.SetActive(active);
		
			if (!initializePresenter) return;
			CarStatusModel statusModel = new CarStatusModel();
			
			// Dispose()：既に_moveableがあるなら
			_moveable?.Dispose();
			_moveable = new UseCaseCarMove(transform, Random.Range(-0.3f, 0.3f), destroyCancellationToken);
			
			// 初期化
			if (statusPresenter) statusPresenter.Initialize(statusModel, ReturnToPoolSubject);
			if (collisionController) collisionController.Initialize(statusModel, statusModel);
			if (poolAutoReturner) poolAutoReturner.Initialize(ReturnToPoolSubject);
		}
		
		public void Update() {
			_moveable.Execute();
		}
	}
}