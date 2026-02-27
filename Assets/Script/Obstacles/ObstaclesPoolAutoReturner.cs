using R3;
using UnityEngine;

namespace Script.Obstacles {
	public class ObstaclesPoolAutoReturner : MonoBehaviour{
		private readonly Subject<Unit> _returnToPoolSubject = new ();
		private bool _isSecondInitialize;		// TODO：フラグ管理どうなんだろうなーというところ

		public void Initialize(Subject<Unit> returnToPoolSubject) {
			// return：2回目以降の初期化なら
			if(_isSecondInitialize) return;
			
			_returnToPoolSubject.Subscribe(returnToPoolSubject.OnNext);

			_isSecondInitialize = true;
		}
		
		private void Update() {
			// プールに戻す：範囲外に出たら
			if(transform.position.x is < -20.0f or > 20.0f ) _returnToPoolSubject.OnNext(Unit.Default);
		}
	}
}