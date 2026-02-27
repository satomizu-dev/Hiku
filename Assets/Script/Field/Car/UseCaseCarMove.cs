using System.Threading;
using R3;
using Script.Obstacles.AttributeInterface;
using UnityEngine;

namespace Script.Field.Car {
	public class UseCaseCarMove : IObstaclesMoveable {
		// 実行処理
		private readonly Subject<Unit> _executeSubject = new();

		public UseCaseCarMove(Transform transform, float moveValue, CancellationToken token) {
			_executeSubject.Subscribe(_ => { transform.position += new Vector3(moveValue, 0, 0); }).RegisterTo(token);
		}

		public void Execute() => _executeSubject.OnNext(Unit.Default);

		public void Dispose() {
			_executeSubject.OnCompleted();
			_executeSubject.Dispose();
		}
	}
}