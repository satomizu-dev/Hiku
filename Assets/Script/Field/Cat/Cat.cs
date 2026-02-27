using Script.Field.Car;
using Script.Obstacles;
using UnityEngine;

namespace Script.Field.Cat {
	public class Cat : Obstacles.Obstacles , IObstacles {
		public void Start() {
			ObstaclesType  = ObstaclesType.Simple;
		}

		public void Initialize(Vector3 position = default, Transform parent = null, bool active = false, bool initializePresenter = false) {
			transform.position = position;
			transform.parent = parent;
			gameObject.SetActive(active);

			if (!initializePresenter) return;
			CarStatusModel statusModel = new CarStatusModel();
			
			if (statusPresenter) statusPresenter.Initialize(statusModel, ReturnToPoolSubject);
			if (collisionController) collisionController.Initialize(statusModel, statusModel);
			if (poolAutoReturner) poolAutoReturner.Initialize(ReturnToPoolSubject);
		}
	}
}