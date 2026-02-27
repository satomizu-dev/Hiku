using MackySoft.Navigathena.SceneManagement;
using UnityEngine.SceneManagement;

namespace Script.Scene {
    // タイトルの遷移管理クラス
    public class TitleSceneEntryPoint:SceneEntryPointBase {
        
        /// <summary>
        /// 遷移処理
        /// </summary>
        public void ChangeToNextScene() {
            SceneManager.LoadScene("InGame");
        }
    }
}
