using R3;
using UnityEngine;

namespace Script.Obstacles {
	public enum ObstaclesType {
		None = 0,
		Simple,
		Move, 
	}
	
	/// <summary>
	/// 障害物のインターフェース
	/// </summary>
	public interface IObstacles {
		/// <summary>
		/// 障害物の種類
		/// </summary>
		public ObstaclesType ObstaclesType { get;}
		
		/// <summary>
		/// プールに戻す
		/// </summary>
		public Observable<Unit> ReturnToPoolObservable { get; }

		/// <summary>
		/// 初期化処理
		/// </summary>
		/// <param name="position"></param>
		/// <param name="parent"></param>
		/// <param name="active"></param>
		/// <param name="initializePresenter"></param>
		public void Initialize(Vector3 position = default, Transform parent = null, bool active = false, bool initializePresenter = false);
	}
}
