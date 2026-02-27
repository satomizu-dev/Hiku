using R3;
using TMPro;
using UnityEngine;

namespace Script.Player {
	/// <summary>
	/// プレイヤーのステータス
	/// </summary>
	public class PlayerStatusPresenter : MonoBehaviour {
		
		private PlayerStatusModel _model;
		
		//  view：チャージ量
		[SerializeField] private TMP_Text viewCharge;
		
		// view：レベル
		[SerializeField] private TMP_Text viewLevel;
		
		/// <summary>
		/// 初期化処理
		/// </summary>
		/// <param name="model"></param>
		public void Initialize(PlayerStatusModel model) {
			if(model == null) return;
			_model = model;

			_model.CounterChargeObservable.Subscribe(_ => {
				if (viewCharge == null) return;
				// テキスト変更
				viewCharge.text = _model.GetChargeNum().ToString();
			}).RegisterTo(destroyCancellationToken);
			
			_model.LevelObservable.Subscribe(_ => {
				if (viewLevel == null) return;
				// テキスト変更
				viewLevel.text = _model.GetLevel().ToString();
			}).RegisterTo(destroyCancellationToken);
		}
	}
}
