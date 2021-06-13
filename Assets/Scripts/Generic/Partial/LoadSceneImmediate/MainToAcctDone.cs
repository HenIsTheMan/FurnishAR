using FurnishAR.Anim;
using UnityEngine;
using UnityEngine.UI;

namespace FurnishAR.Generic {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		public static void MainToAcctDone() {
			GameObject.Find("AcctButton").GetComponent<Button>().onClick.AddListener(() => {
				bool shldReturn = false;

				RectTransformAnchoredTranslateAnim[] anims = GameObject.Find("MainPanel").GetComponents<RectTransformAnchoredTranslateAnim>();
				foreach(var anim in anims) {
					if(anim.IsUpdating) {
						shldReturn = true;
						break;
					}
				}

				if(shldReturn) {
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