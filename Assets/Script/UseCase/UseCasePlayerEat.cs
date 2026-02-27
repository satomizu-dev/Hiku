using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using Script.Player;
using UnityEngine;

namespace Script.UseCase {
	/// <summary>
	/// 障害物の破壊時に座標同期させる動作実装
	/// </summary>
	public class UseCasePlayerEat : IUseCasePlayer {
		// 実行処理
		private readonly Subject<Vector3> _executeSubject = new();

		public UseCasePlayerEat(PlayerUseCaseWatcher useCaseWatcher, GameObject player, float completeTime, CancellationToken cancellationToken) {
			_executeSubject.SubscribeAwait(async (endValue, token) => await Eat(endValue, token)).RegisterTo(cancellationToken);
			return;

			async UniTask Eat(Vector3 endValue, CancellationToken token) {
				// return：前の動作が実行中だったら
				if(useCaseWatcher.IsPrevUseCaseExecuting()) return;
				
				// 現在の動作を自身に
				useCaseWatcher.InitUseCase(this);
				
				Transform transform = player.transform;
				// 移動量：z軸のみに限定
				endValue = new Vector3(0, 0, endValue.z);
				
				// player：敵の座標に移動
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