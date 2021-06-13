using FurnishAR.Anim;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FurnishAR.Generic {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		public static void SplashToIntroPrelim() {
			GameObject.Find("FiguratiText").GetComponent<TextFadeAnim>().animEndDelegate += SubSplashToIntroPrelim;
		}

		private static void SubSplashToIntroPrelim() {
			if(!globalObj.canClickOnSplash) {
				return;
			}

			GameObject proxyCamGO = GameObject.Find("ProxyCam");

			RectTransformRotateAnim rectTransformRotateAnim = proxyCamGO.GetComponent<RectTransformRotateAnim>();

			rectTransformRotateAnim.animEndDelegate += SubSplashToIntroFadeTransitionPrelim;

			_ = globalObj.StartCoroutine(globalObj.MyFunc(proxyCamGO, rectTransformRotateAnim));
		}

		private IEnumerator MyFunc(GameObject proxyCamGO, RectTransformRotateAnim rectTransformRotateAnim) {
			yield return new WaitForSeconds(4.0f);

			if(rectTransformRotateAnim != null) {
				rectTransformRotateAnim.IsUpdating = true;
				GameObject.Find("SplashWhooshOut").GetComponent<AudioSource>().Play();
			}
			if(proxyCamGO != null) {
				proxyCamGO.GetComponents<RectTransformScaleAnim>()[1].IsUpdating = true;
			}
		}

		private static void SubSplashToIntroFadeTransitionPrelim() {
			globalObj.canClickOnSplash = false;

			PtrManager ptrManager = FindObjectOfType<PtrManager>();
			ptrManager.DeactivateAllPtrTrails();

			GameObject camGO = GameObject.Find("IntroCam");
			Camera camComponent = camGO.GetComponent<Camera>();
			camComponent.enabled = true;

			CanvasGrpFadeAnim canvasGrpFadeAnim = GameObject.Find("IntroCanvas").GetComponent<CanvasGrpFadeAnim>();
			GameObject.Find("SplashCanvas").GetComponent<CanvasGrpFadeAnim>().IsUpdating = true;
			canvasGrpFadeAnim.IsUpdating = true;

			canvasGrpFadeAnim.animEndDelegate += () => {
				camComponent.clearFlags = CameraClearFlags.SolidColor;

				globalObj.sceneManager.UnloadScene(
					UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
					UnloadSceneTypes.UnloadSceneType.UnloadAllEmbeddedSceneObjs,
					() => {
						camGO.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = true;
						ptrManager.camComponent = FindObjectOfType<Camera>();
					}
				);
			};
		}
	}
}