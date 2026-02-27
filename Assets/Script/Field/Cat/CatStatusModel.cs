using R3;
using Script.Obstacles;
using Script.Obstacles.AttributeInterface;

namespace Script.Field.Cat {
	public class CatStatusModel : IObstaclesStatusModel, IObstaclesDamageable, IObstaclesChargeable{
		private readonly ReactiveProperty<int> _hp = new();
		private readonly ReactiveProperty<int> _chargeAmount = new();
		public int CurrentHp => _hp.Value;
		public int MaxHp { get; }
		public int GetScoreNum() =>CurrentHp * 100;
		public int GetChargeAmount() => _chargeAmount.Value;
		
		public Observable<Unit> DieObservable => _dieSubject;
		private readonly Subject<Unit> _dieSubject = new ();
		public void Die() => _dieSubject.OnNext(Unit.Default);

		public CatStatusModel(int hp = 1, int chargeAmount = 1) {
			_hp.Value = hp;
			MaxHp = _hp.Value;
			_chargeAmount.Value = chargeAmount;
		}
		
		public void Damage(int amount = 1) {
			// return：HP以上のダメージでなければ
			if (amount < _hp.Value) return;
			
			// HP：０に
			_hp.Value = 0;
		}
		
		public void Dispose() {
			_dieSubject.OnCompleted();
			_dieSubject.Dispose();
			
			_chargeAmount.OnCompleted();
			_chargeAmount.Dispose();
			
			_hp.OnCompleted();
			_hp.Dispose();
		}
	}
}