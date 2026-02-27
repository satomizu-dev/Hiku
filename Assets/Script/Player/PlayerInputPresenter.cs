using JetBrains.Annotations;
using R3;
using Script.UseCase;
using UnityEngine;

namespace Script.Player {
	/// <summary>
	/// presenter：入力管理
	/// </summary>
	public class PlayerInputPresenter : MonoBehaviour{
		/// <summary>
		/// 初期化処理
		/// </summary>
		public void Initialize(PlayerInputModel model, IUseCasePlayer useCaseCharge, IUseCasePlayer useCaseAttack, IUseCasePlayer useCaseReposition) {
			if(model == null) return;
			if(useCaseCharge != null) model.ChargeObservable.Subscribe(useCaseCharge.Execute).RegisterTo(destroyCancellationToken);
			if(useCaseAttack != null) model.AttackObservable.Subscribe(useCaseAttack.Execute).RegisterTo(destroyCancellationToken);
			if(useCaseReposition != null) model.RepositionObservable.Subscribe(useCaseReposition.Execute).RegisterTo(destroyCancellationToken);
		}
	}
}