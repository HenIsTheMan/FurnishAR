using UnityEngine;

namespace IWP.Generic {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		internal static void OnSplashClick() {
			if(globalObj.canClickOnSplash && UnityEngine.SceneManagement.SceneManager.GetSceneByName("IntroScene").isLoaded) {
				GameObject.Find("Feathers").SetActive(false);
				GameObject.Find("ProxyCam").SetActive(false);

				SubSplashToIntroFadeTransitionPrelim();

				globalObj.canClickOnSplash = false;
			}
		}
	}
}