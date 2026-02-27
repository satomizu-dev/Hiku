using Script.Obstacles;
using Script.Obstacles.AttributeInterface;
using UnityEngine;

namespace Script.CollisionController {
	public interface IObstaclesCollision : IObstaclesDamageable, IObstaclesChargeable {
		public Vector3 GetOriginPosition();
	}
	
	/// <summary>
	/// PlayerCollisionControllerで検知され、障害物にアクセスする橋渡しクラス
	/// Colliderがアタッチされてるオブジェクトにアタッチして使う
	/// </summary>
	public class ObstaclesCollisionController : MonoBehaviour, IObstaclesCollision {
		
		// 原点の座標
		public Vector3 GetOriginPosition() => originTransform.position;
		[SerializeField] private Transform originTransform;
		
		// model：ステータス
		// TODO：本当はmodelを持ちたくないと思ったり
		private IObstaclesDamageable _damageable;
		private IObstaclesChargeable _chargeable;
		
		public void Damage(int amount = 1) => _damageable.Damage(amount);
		public int CurrentHp => _damageable.CurrentHp;
		public int MaxHp => _damageable.MaxHp;
		public int GetScoreNum() => MaxHp * 100;
		public void Die() => _damageable.Die();
		public int GetChargeAmount() => _chargeable.GetChargeAmount();

		/// <summary>
		/// 初期化処理
		/// </summary>
		/// <param name="damageable"></param>
		/// <param name="chargeable"></param>
		public void Initialize(IObstaclesDamageable damageable, IObstaclesChargeable chargeable) {
			if (damageable != null) _damageable = damageable;
			if (chargeable != null) _chargeable = chargeable;
		}
	}
}
