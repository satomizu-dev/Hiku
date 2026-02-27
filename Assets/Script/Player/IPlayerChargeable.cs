namespace Script.Player {
	/// <summary>
	/// interface：チャージ
	/// </summary>
	public interface IPlayerChargeable {
		/// <summary>
		/// 獲得
		/// </summary>
		/// <returns></returns>
		public int GetChargeNum();
		
		/// <summary>
		/// 加算
		/// </summary>
		public void AddCharge(int amount = 1);

		/// <summary>
		/// 「レベル」獲得
		/// </summary>
		/// <returns></returns>
		public int GetLevel();
		
		/// <summary>
		/// 「攻撃力」獲得：「チャージ」消費前の値で獲得
		/// 　　　　　　　：消費後にダメージ判定を行うが、操作的に直剣的でないため
		/// </summary>
		/// <returns></returns>
		public int GetAttackAmount();

		/// <summary>
		/// 「チャージ」消費
		/// </summary>
		/// <param name="useAmount"></param>
		/// <param name="addScoreAmount"></param>
		public void UseCharge(int useAmount = 1, int addScoreAmount = 0);
	}
}
