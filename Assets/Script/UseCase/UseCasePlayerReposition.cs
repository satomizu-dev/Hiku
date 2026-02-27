using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using Script.Player;
using UnityEngine;

namespace Script.UseCase {
	public class UseCasePlayerReposition : IUseCasePlayer {
		
		// 実行処理
		private readonly Subject<Vector3> _executeSubject = new();

		public UseCasePlayerReposition(PlayerUseCaseWatcher useCaseWatcher, IPlayerChargeable chargeable, float completeTime, Transform transform, CancellationToken cancellationToken) {
			_executeSubject.SubscribeAwait(async (moveVector, token) => await RepositionAsync(moveVector, token)).RegisterTo(cancellationToken);
			return;
			
			// 移動処理
			async UniTask RepositionAsync(Vector3 moveVector, CancellationToken token) {
				// return：前の動作が実行中だったら
				if(useCaseWatcher.IsPrevUseCaseExecuting()) return;
				
				// return：チャージ量が無かったら
				if(chargeable.GetChargeNum() == 0) return;
				
				// 移動制限
				switch (transform.position.x) {
					case <= -3.5f when moveVector == Vector3.left:
					case >= 3.5f when moveVector == Vector3.right: return; }

				// 現在の動作を自身に
				useCaseWatcher.InitUseCase(this);
				
				// 移動量
				Vector3 endValue = transform.position + moveVector;
								
				// チャージ量消費
				chargeable.UseCharge();
				
				// 移動
				Tween tween = transform.DOMove(endValue, completeTime).SetEase(Ease.OutQuad);
				await using (token.Register(() => tween.Kill())) await tween.AsyncWaitForCompletion();
				
				// 待機状態へ
				useCaseWatcher.ResetUseCase();
			}
		}
		
		public void Execute(Unit unit) {}
		public void Execute(Vector3 vector3) => _executeSubject.OnNext(vector3);
	}
}