using System;

namespace Script.Obstacles.AttributeInterface {
	public interface IObstaclesMoveable : IDisposable {
		/// <summary>
		/// 実行処理
		/// </summary>
		public void Execute();
	}
}