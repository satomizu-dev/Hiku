using R3;
using UnityEngine;

namespace Script.Obstacles {
	public class ObstaclesStatusPresenter : MonoBehaviour, IObstaclesStatusPresenter {
		// model：ステータス管理
		private IObstaclesStatusModel _model;
		
		public void Initialize(IObstaclesStatusModel model, Subject<Unit> returnToPoolSubject) {
			if(model == null) return;
			
			// Dispose()：既にmodelがある = 一度生成されており、オブジェクトプールによって使いまわされる場合
			_model?.Dispose();
			
			_model = model;
			_model.DieObservable.Subscribe(returnToPoolSubject.OnNext);
		}
	}
}