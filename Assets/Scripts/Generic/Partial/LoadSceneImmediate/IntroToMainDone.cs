using UnityEngine;
using UnityEngine.UI;

namespace FurnishAR.Generic {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		public static void IntroToMainDone() {
			GameObject.Find("SkipButton").GetComponent<Button>().onClick.AddListener(() => {
				globalObj.sceneManager.UnloadScene(
					UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
					UnloadSceneTypes.UnloadSceneType.UnloadAllEmbeddedSceneObjs,
					() => {
						UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[0].SetActive(true);
					}
				);
			});
		}
	}
}