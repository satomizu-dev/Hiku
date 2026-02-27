using System.Threading;
using Script.Scriptable;
using Script.UseCase;
using UnityEngine;

namespace Script.Player {
	public class PlayerDependencyResolver {
		public PlayerAnimationModel AnimationModel { get; }
		public PlayerStatusModel StatusModel { get; }
		public PlayerInputModel InputModel { get; }
		public UseCasePlayerCharge  UseCasePlayerCharge { get; }
		public UseCasePlayerAttack UseCasePlayerAttack { get; }
		public UseCasePlayerEat UseCasePlayerEat { get; }
		public UseCasePlayerDead UseCasePlayerDead { get; }
		public UseCasePlayerReposition UseCasePlayerReposition { get; }
		
		public PlayerDependencyResolver(GameObject gameObject, PlayerScriptableObject scriptableObject, CancellationToken cancellationToken) {
			AnimationModel = new PlayerAnimationModel(0.5f, 0.03f, 0.07f);
			StatusModel = new PlayerStatusModel(scriptableObject);
			InputModel = new PlayerInputModel();

			PlayerUseCaseWatcher useCaseWatcher = new PlayerUseCaseWatcher();
			
			UseCasePlayerCharge = new UseCasePlayerCharge(useCaseWatcher, gameObject, StatusModel, AnimationModel, cancellationToken);
			UseCasePlayerAttack = new UseCasePlayerAttack(useCaseWatcher, StatusModel, 0.1f, gameObject.transform, cancellationToken);
			UseCasePlayerEat = new UseCasePlayerEat(useCaseWatcher, gameObject, 0.05f, cancellationToken);
			UseCasePlayerDead = new UseCasePlayerDead(useCaseWatcher, gameObject, cancellationToken);
			UseCasePlayerReposition = new UseCasePlayerReposition(useCaseWatcher, StatusModel, 0.2f, gameObject.transform, cancellationToken);
		}
	}
}
