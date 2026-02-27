using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Script.Player {
    public class PlayerAnimationPresenter : MonoBehaviour {
        [SerializeField] private float endValue;
        private PlayerAnimationModel _model;
        
        /// <summary>
        ///  初期化処理
        /// </summary>
        /// <param name="model"></param>
        public void Initialize(PlayerAnimationModel  model) {
            if(model == null) return;
            _model = model;
        }
    }
}
