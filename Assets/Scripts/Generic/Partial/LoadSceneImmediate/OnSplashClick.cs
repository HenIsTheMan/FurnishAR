using FurnishAR.App;
using UnityEngine;

namespace FurnishAR.Generic {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		internal static void OnSplashClick() {
			if(globalObj.canClickOnSplash
				&& UnityEngine.SceneManagement.SceneManager.GetSceneByName("IntroScene").isLoaded
			) {
				AudioCentralControl.globalObj.StopAllAudio();

				GameObject.Find("ProxyCam").SetActive(false);

				SubSplashToIntroFadeTransitionPrelim();

				globalObj.canClickOnSplash = false;
			}
		}
	}
}