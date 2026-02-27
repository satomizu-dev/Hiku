using R3;
using UnityEngine;

namespace Script.UseCase {
	/// <summary>
	/// interface：プレイヤーの動作実装
	/// // TODO：Executeが乱立している状況、何とかしたい
	/// </summary>
	public interface IUseCasePlayer {
		public void Execute(Unit unit);
		public void Execute(Vector3 position);
	}
}