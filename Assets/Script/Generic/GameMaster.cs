using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Generic {
	public class GameMaster : SingletonMonoBehaviour<GameMaster> {
		protected override bool IsDontDestroyOnLoad => false;

		private readonly Subject<Unit> _gameEndSubject = new();

		protected override void Awake() {
			_gameEndSubject.Subscribe(_ => {
				Debug.Log("Game End");
				SceneManager.LoadScene("InGame");
			}).RegisterTo(destroyCancellationToken);
			
			base.Awake();
		}

		public void GameEnd() => _gameEndSubject.OnNext(Unit.Default);
	}
}