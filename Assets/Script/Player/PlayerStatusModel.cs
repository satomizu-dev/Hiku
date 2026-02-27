using System.Collections.Generic;
using R3;
using Script.Scriptable;
using UnityEngine;

namespace Script.Player {
	/// <summary>
	/// model：ステータス管理
	/// </summary>
	public class PlayerStatusModel : IPlayerChargeable , IPlayerScoreable {

		private readonly List<int> _borderLevelList;
		
		// スコア量
		public Observable<int> ScoreObservable => _score;
		private readonly ReactiveProperty<int> _score =new (0);
		
		// 「チャージ」の量
		public Observable<int> CounterChargeObservable => _counterCharge;
		private readonly ReactiveProperty<int> _counterCharge  = new ();
		private int _useChargeAmount;
		
		// レベル
		public Observable<int> LevelObservable => _levelCharge;
		private readonly ReactiveProperty<int> _levelCharge  = new ();

		public PlayerStatusModel(PlayerScriptableObject scriptableObject) {
			_borderLevelList = scriptableObject.borderLevelList;
		}

		private void CalculateLevel() {
			int index = _borderLevelList.FindIndex(border => GetChargeNum() < border); 
			_levelCharge.Value =  index == -1 ? _borderLevelList.Count : index;
		}
		
		public int GetChargeNum() => _counterCharge.Value;
		public void AddCharge(int amount = 1) { _counterCharge.Value = Mathf.Max(GetChargeNum() + amount, 0); CalculateLevel(); }
		public int GetLevel() => _levelCharge.Value;
		public int GetAttackAmount() => GetChargeNum() + _useChargeAmount;
		public void UseCharge(int useAmount = 1, int addScoreAmount = 0) {
			_useChargeAmount = useAmount;
			AddCharge(-_useChargeAmount);
			AddScore(addScoreAmount);				// スコア追加があれば：連続移動ボーナスなど
		}
		
		public int GetScore() => _score.Value;
		public void AddScore(int amount) => _score.Value += amount;
	}
}
