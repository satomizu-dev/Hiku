using System;
using R3;

namespace Script.Obstacles {
	public interface IObstaclesStatusModel : IDisposable {
		/// <summary>
		/// 死亡時の処理
		/// </summary>
		public Observable<Unit> DieObservable { get; }
	}
}