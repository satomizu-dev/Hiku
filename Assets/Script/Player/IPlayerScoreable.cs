using R3;

namespace Script.Player {
	/// <summary>
	/// interface：スコア
	/// </summary>
	public interface IPlayerScoreable {
		public Observable<int> ScoreObservable { get; }
		/// <summary>
		/// 獲得
		/// </summary>
		/// <returns></returns>
		public int GetScore();
		/// <summary>
		/// 加算
		/// </summary>
		/// <param name="amount"></param>
		public void AddScore(int amount);
	}
}