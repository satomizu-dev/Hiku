using System.Threading;
using R3;
using Script.Player;
using UnityEngine;

namespace Script.UseCase {
	/// <summary>
	/// 「もうひとつ」の動作実装
	/// </summary>
	public class UseCasePlayerCharge : IUseCasePlayer {
		// 実行処理
		private readonly Subject<Unit> _executeSubject = new();
		
		public UseCasePlayerCharge(PlayerUseCaseWatcher useCaseWatcher, GameObject target, IPlayerChargeable chargeable, PlayerAnimationModel animationModel, CancellationToken cancellationToken) {
			_executeSubject.SubscribeAwait(async (_, token) => {
				// return：前の動作が実行中だったら
				if(useCaseWatcher.IsPrevUseCaseExecuting()) return;
				
				// 現在の動作を自身に
				useCaseWatcher.InitUseCase(this);
				
				// 回復
				chargeable.AddCharge();
				// アニメーション再生
				await animationModel.DoAnimationAsync(target, token);
				
				// 待機状態へ
				useCaseWatcher.ResetUseCase();
			}).RegisterTo(cancellationToken);
		}
		
		public void Execute(Unit unit) => _executeSubject.OnNext(unit);
		public void Execute(Vector3 vector3) {}
	}
}