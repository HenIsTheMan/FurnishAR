using UnityEngine;
using UnityEngine.UI;

namespace FurnishAR.Generic {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		public static void MainToMenuDone() {
			GameObject.Find("MenuButton").GetComponent<Button>().onClick.AddListener(() => {
				globalObj.sceneManager.UnloadScene(
					UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
					UnloadSceneTypes.UnloadSceneType.UnloadAllEmbeddedSceneObjs,
					null
				);
			});
		}
	}
}