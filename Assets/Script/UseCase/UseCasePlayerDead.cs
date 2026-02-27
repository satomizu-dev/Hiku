using System.Threading;
using R3;
using Script.Generic;
using UnityEngine;

namespace Script.UseCase {
	public class UseCasePlayerDead : IUseCasePlayer{
		// 実行処理
		private readonly Subject<Unit> _executeSubject = new();

		public UseCasePlayerDead(PlayerUseCaseWatcher useCaseWatcher, GameObject player, CancellationToken cancellationToken) {
			_executeSubject.Subscribe(_ => {
				// return：プレイヤーが移動中なら
				if(useCaseWatcher.CurrentUseCasePlayer != null && 
				   useCaseWatcher.CurrentUseCasePlayer.GetType() == typeof(UseCasePlayerAttack)) return;
				
				// 現在の動作を自身に
				useCaseWatcher.InitUseCase(this);
				
				// player：死亡
				Object.Destroy(player);
				
				// ゲーム終了
				GameMaster.Instance.GameEnd();
				
			}).RegisterTo(cancellationToken);
		}
		
		public void Execute(Unit unit) => _executeSubject.OnNext(Unit.Default);
		public void Execute(Vector3 vector3) {}
	}
}