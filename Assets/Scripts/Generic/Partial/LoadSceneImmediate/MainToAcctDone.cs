using FurnishAR.App;
using UnityEngine;
using UnityEngine.UI;

namespace FurnishAR.Generic {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		public static void MainToAcctDone() {
			GameObject.Find("AcctButton").GetComponent<Button>().onClick.AddListener(() => {
				if(GameObject.Find("ThinUpArrowButton") != null) { //If ThinUpArrowButton is inactive and hence cannot be found...
					return;
				}

				UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[0].SetActive(false);

				UnityEngine.SceneManagement.SceneManager.SetActiveScene(
					UnityEngine.SceneManagement.SceneManager.GetSceneByName("AcctScene")
				);

				UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[0].SetActive(true);
			});
		}
	}
}