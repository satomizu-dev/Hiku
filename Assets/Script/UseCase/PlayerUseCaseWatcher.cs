using UnityEngine;

namespace Script.UseCase {
	public class PlayerUseCaseWatcher{
		public IUseCasePlayer CurrentUseCasePlayer { get; private set; } = new UseCasePlayerIdle();

		/// <summary>
		/// 過去の動作実行中かどうか
		/// </summary>
		/// <returns></returns>
		public bool IsPrevUseCaseExecuting() => CurrentUseCasePlayer.GetType() != typeof(UseCasePlayerIdle);

		/// <summary>
		/// 動作実行の準備
		/// </summary>
		/// <param name="useCasePlayer"></param>
		public void InitUseCase(IUseCasePlayer useCasePlayer) {
			if (useCasePlayer == null) return;
			Debug.Log($"初期化：{useCasePlayer.GetType()}");
			CurrentUseCasePlayer = useCasePlayer;
		}
		
		/// <summary>
		/// 動作をリセット
		/// </summary>
		public void ResetUseCase() {
			Debug.Log("状態リセット");
			CurrentUseCasePlayer = new UseCasePlayerIdle();
		}
	}
}