namespace Script.Obstacles.AttributeInterface {
	/// <summary>
	/// ダメージを受けるオブジェクトのインターフェース		
	/// </summary>
	public interface IObstaclesDamageable {
		/// <summary>
		/// ダメージを受ける
		/// </summary>
		/// <param name="amount"></param>
		public void Damage(int amount = 1);

		/// <summary>
		/// HP獲得：現在
		/// </summary>
		/// <returns></returns>
		public int CurrentHp { get; }

		/// <summary>
		/// HP獲得：最大
		/// </summary>
		/// <returns></returns>
		public int MaxHp { get; }

		/// <summary>
		/// スコア獲得：HP×100
		/// </summary>
		/// <returns></returns>
		public int GetScoreNum();

		/// <summary>
		/// 死亡処理
		/// </summary>
		public void Die();
	}
}