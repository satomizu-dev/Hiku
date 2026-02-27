using System;
using System.Linq;
using R3;
using Script.Obstacles.AttributeInterface;
using Script.Player;
using Script.UseCase;
using UnityEngine;
using Object = System.Object;

namespace Script.CollisionController {
	public interface IPlayerCollision {
	}
	
	/// <summary>
	/// プレイヤーの当たり判定時、ステータスなどにアクセスする橋渡しクラス
	/// Colliderがアタッチされてるオブジェクトにアタッチして使う
	/// </summary>
	public class PlayerCollisionController : MonoBehaviour,  IPlayerCollision {
		
		// 回復処理
		private readonly Subject<int> _chargeableSubject = new();
		
		// スコア処理
		private readonly Subject<int> _scoreableSubject = new();

		// player：移動処理
		private readonly Subject<Vector3> _eatObstaclesSubject = new();
		
		// player：死亡処理
		private readonly Subject<Unit> _deadSubject = new();
		
		// obstacles：ダメージ処理
		private readonly Subject<IObstaclesDamageable> _obstaclesDamageableSubject = new();

		/// <summary>
		/// 初期化処理
		/// </summary>
		/// <param name="chargeable"></param>
		/// <param name="scoreable"></param>
		/// <param name="useCasePlayerEat"></param>
		/// <param name="useCaseDead"></param>
		public void Initialize(IPlayerChargeable chargeable, IPlayerScoreable scoreable, IUseCasePlayer useCasePlayerEat, IUseCasePlayer useCaseDead) {
			if (chargeable != null) {
				_chargeableSubject.Subscribe(chargeable.AddCharge).RegisterTo(destroyCancellationToken);
				_obstaclesDamageableSubject.Subscribe(damageable => damageable.Damage(chargeable.GetAttackAmount()));
			}
			if (scoreable != null) _scoreableSubject.Subscribe(scoreable.AddScore).RegisterTo(destroyCancellationToken);
			if (useCasePlayerEat != null) _eatObstaclesSubject.Subscribe(useCasePlayerEat.Execute).RegisterTo(destroyCancellationToken);
			if(useCaseDead !=  null) _deadSubject.Subscribe(useCaseDead.Execute).RegisterTo(destroyCancellationToken);
		}

		public void OnTriggerEnter(Collider other) {
			// return：障害物じゃないなら
			if(!other.TryGetComponent(out IObstaclesCollision obstacle)) return;
			
			// player：移動中でないなら死亡
			_deadSubject.OnNext(Unit.Default);
			
			Type[] typeArray =obstacle.GetType().GetInterfaces();
			
			// return：ダメージを与えられないなら
			if (typeArray.All(type => type != typeof(IObstaclesDamageable))) return;
			
			// Obstacles：ダメージ
			_obstaclesDamageableSubject.OnNext(obstacle);
				
			// return：破壊されてなければ
			if(obstacle.CurrentHp > 0) return;
			
			// 死亡前に必要な情報を取得
			Vector3 position = obstacle.GetOriginPosition();

			// 死亡通知
			obstacle.Die();
			
			// スコア加算
			_scoreableSubject.OnNext(obstacle.GetScoreNum());
				
			// player：座標移動
			_eatObstaclesSubject.OnNext(position);
				
			// return：プレイヤーを回復できないなら
			if (typeArray.All(type => type != typeof(IObstaclesChargeable))) return;
			
			// player：チャージ
			_chargeableSubject.OnNext(obstacle.GetChargeAmount());
		}
	}
}
