using R3;
using TMPro;
using UnityEngine;

namespace Script.Player {
	public class PlayerScorePresenter : MonoBehaviour{
		[SerializeField] private TMP_Text text;
		private IPlayerScoreable _model;
		
		/// <summary>
		///  初期化処理
		/// </summary>
		/// <param name="model"></param>
		public void Initialize(IPlayerScoreable  model) {
			if(model == null) return;
			_model = model;
			
			_model.ScoreObservable.Subscribe(score => {
				if (text == null) return;
				
				// テキスト変更
				text.text = _model.GetScore().ToString();
			}).RegisterTo(destroyCancellationToken);
		}
	}
}