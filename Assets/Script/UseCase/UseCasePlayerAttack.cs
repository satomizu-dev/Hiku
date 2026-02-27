using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using Script.Player;
using UnityEngine;

namespace Script.UseCase {
	/// <summary>
	/// 移動する動作実装
	/// </summary>
	public class UseCasePlayerAttack : IUseCasePlayer{
		// 実行処理
		private readonly Subject<Unit> _executeSubject = new();
		
		public UseCasePlayerAttack(PlayerUseCaseWatcher useCaseWatcher, IPlayerChargeable chargeable, float completeTime, Transform transform, CancellationToken cancellationToken) {
			_executeSubject.SubscribeAwait(async (_, token) => await AttackAsync(token)).RegisterTo(cancellationToken);
			return;
			
			// 移動処理
			async UniTask AttackAsync(CancellationToken token) {
				// return：前の動作が実行中だったら
				if(useCaseWatcher.IsPrevUseCaseExecuting()) return;
				
				// return：チャージ量が無かったら
				if(chargeable.GetChargeNum() == 0) return;
				
				// 現在の動作を自身に
				useCaseWatcher.InitUseCase(this);
				
				// 移動量：チャージ量を考慮する
				int playerLevel = chargeable.GetLevel();
				Vector3 endValue = transform.position + Vector3.forward * playerLevel;
				
				// 連続移動ボーナス
				int addScoreAmount = 10;
				
				// チャージ量消費
				chargeable.UseCharge(addScoreAmount: addScoreAmount);
				
				// 移動
				Tween tween = transform.DOMove(endValue, completeTime).SetEase(Ease.OutQuad);
				await using (token.Register(() => tween.Kill())) await tween.AsyncWaitForCompletion();
				
				// 待機状態へ
				useCaseWatcher.ResetUseCase();
			}
		}
		
		public void Execute(Unit unit) => _executeSubject.OnNext(Unit.Default);
		public void Execute(Vector3 vector3) {}
	}
}