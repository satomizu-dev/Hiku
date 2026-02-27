using Script.CollisionController;
using Script.Scriptable;
using UnityEngine;

namespace Script.Player {
    public class PlayerController : MonoBehaviour {
        // 「もうひとつ」のプレゼンター
        [SerializeField] private PlayerStatusPresenter statusPresenter;
        
        // アニメーションのプレゼンター
        [SerializeField] private PlayerAnimationPresenter animationPresenter;
        
        // 当たり判定の管理クラス
        [SerializeField] private PlayerCollisionController collisionController;
        
        // スコアの管理クラス
        [SerializeField] private PlayerScorePresenter scorePresenter;
        
        // 入力処理の管理クラス
        [SerializeField] private PlayerInputPresenter inputPresenter;
        
        // ScriptableObject
        [SerializeField] private PlayerScriptableObject scriptableObject;
        
        // 依存性解決
        private PlayerDependencyResolver _dependencyResolver;
        
        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Start() {
            _dependencyResolver = new PlayerDependencyResolver(gameObject, scriptableObject, destroyCancellationToken);
            PlayerStatusModel statusModel = _dependencyResolver.StatusModel;
            
            if(animationPresenter) animationPresenter.Initialize(_dependencyResolver.AnimationModel);
            if(statusPresenter) statusPresenter.Initialize(statusModel);
            if(scorePresenter) scorePresenter.Initialize(statusModel);
            if (collisionController) collisionController.Initialize(statusModel, statusModel, _dependencyResolver.UseCasePlayerEat, _dependencyResolver.UseCasePlayerDead);
            if(inputPresenter) inputPresenter.Initialize(_dependencyResolver.InputModel, _dependencyResolver.UseCasePlayerCharge, _dependencyResolver.UseCasePlayerAttack, _dependencyResolver.UseCasePlayerReposition);
        }
    }
}
