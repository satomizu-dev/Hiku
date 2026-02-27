using UnityEngine;

namespace Script.Obstacles.Hard {
	public class ObstaclesHard : Obstacles , IObstacles {
		
		public void Start() {
			//ObstaclesType = ObstaclesType.Hard;
		}
		
		public void Initialize(Vector3 position = default, Transform parent = null, bool active = false, bool initializePresenter = false) {
			transform.position = position;
			transform.parent = parent;
			gameObject.SetActive(active);
			
			if (!initializePresenter) return;
			ObstaclesHardStatusModel statusModel = new ObstaclesHardStatusModel(2);
			
			if (statusPresenter) statusPresenter.Initialize(statusModel, ReturnToPoolSubject);
			if (collisionController) collisionController.Initialize(statusModel, statusModel);
			if (poolAutoReturner) poolAutoReturner.Initialize(ReturnToPoolSubject);
		}
	}
}