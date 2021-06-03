using UnityEngine;
using UnityEngine.UI;

namespace FurnishAR.Generic {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		public static void MainToAcctDone() {
			GameObject.Find("AcctButton").GetComponent<Button>().onClick.AddListener(() => {
				UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[0].SetActive(false);

				UnityEngine.SceneManagement.SceneManager.SetActiveScene(
					UnityEngine.SceneManagement.SceneManager.GetSceneByName("AcctScene")
				);

				UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[0].SetActive(true);

				GameObject camGO = GameObject.Find("AcctCam");
				Camera camComponent = camGO.GetComponent<Camera>();
				camComponent.enabled = true;
				camComponent.clearFlags = CameraClearFlags.SolidColor;
			});
		}
	}
}