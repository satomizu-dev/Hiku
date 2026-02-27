using R3;
using Script.UseCase;
using UnityEngine;

namespace Script.Player {
	/// <summary>
	/// model：入力管理
	/// </summary>
	public class PlayerInputModel {
		private readonly PlayerInputActions _inputActions = new ();
		
		public Observable<Unit> ChargeObservable => _actionChargeSubject;
		private readonly Subject<Unit> _actionChargeSubject= new ();
		
		public Observable<Unit> AttackObservable => _attackSubject;
		private readonly Subject<Unit> _attackSubject= new ();
		
		public Observable<Vector3> RepositionObservable => _repositionSubject;
		private readonly Subject<Vector3> _repositionSubject= new ();
		
		private PlayerUseCaseWatcher _useCaseWatcher;
		
		public PlayerInputModel() {
			// "Charge"
			_inputActions.Player.Charge.performed += _ => _actionChargeSubject.OnNext(Unit.Default);
			// "Attack"
			_inputActions.Player.Attack.performed += _ => _attackSubject.OnNext(Unit.Default);
			// "Back"
			_inputActions.Player.RepositionBack.performed += _ => _repositionSubject.OnNext(Vector3.back);
			// "Right"
			_inputActions.Player.RepositionRight.performed += _ => _repositionSubject.OnNext(Vector3.right);
			// "Left"
			_inputActions.Player.RepositionLeft.performed += _ => _repositionSubject.OnNext(Vector3.left);
			
			_inputActions.Player.Enable();
		}
		
		~PlayerInputModel() {
			_inputActions.Player.Disable();
		}
		
	}
}