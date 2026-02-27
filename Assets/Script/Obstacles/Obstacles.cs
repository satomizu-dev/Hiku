using R3;
using Script.CollisionController;
using UnityEngine;

namespace Script.Obstacles {
	public class Obstacles : MonoBehaviour {
		// ステータス管理
		[SerializeField]
		protected ObstaclesStatusPresenter statusPresenter;
		
		// 当たり判定管理
		[SerializeField]
		protected ObstaclesCollisionController collisionController;
		
		// 当たり判定管理
		[SerializeField]
		protected ObstaclesPoolAutoReturner poolAutoReturner;
		
		public ObstaclesType ObstaclesType { get; protected set; }
		
		public Observable<Unit> ReturnToPoolObservable => ReturnToPoolSubject;
		protected readonly Subject<Unit> ReturnToPoolSubject = new();
		
	}
}