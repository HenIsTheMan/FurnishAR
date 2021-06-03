using UnityEngine;
using UnityEngine.UI;

namespace FurnishAR.Generic {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		public static void MainToAcctDone() {
			GameObject.Find("MenuButton").GetComponent<Button>().onClick.AddListener(() => {
				globalObj.sceneManager.UnloadScene(
					UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
					UnloadSceneTypes.UnloadSceneType.UnloadAllEmbeddedSceneObjs,
					() => {
						UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[0].SetActive(true);

						GameObject camGO = GameObject.Find("AcctCam");
						Camera camComponent = camGO.GetComponent<Camera>();
						camComponent.enabled = true;
						camComponent.clearFlags = CameraClearFlags.SolidColor;
					}
				);
			});
		}
	}
}