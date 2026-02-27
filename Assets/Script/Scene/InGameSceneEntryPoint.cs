using MackySoft.Navigathena.SceneManagement;
using UnityEngine.SceneManagement;

namespace Script.Scene {
	// インゲームの遷移管理クラス
	public class InGameSceneEntryPoint:SceneEntryPointBase {

		/// <summary>
		/// 遷移処理
		/// </summary>
		public void ChangeToNextScene() {
			SceneManager.LoadScene("Title");
		}
	}
}