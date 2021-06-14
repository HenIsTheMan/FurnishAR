using FurnishAR.Anim;
using FurnishAR.App;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FurnishAR.Generic {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		public static void SplashToIntroPrelim() {
			TextFadeAnim textFadeAnim = GameObject.Find("FiguratiText").GetComponent<TextFadeAnim>();
			textFadeAnim.animEndDelegate += () => {
				AudioCentralControl.globalObj.PlayAudio("SplashHumIn");
				SubSplashToIntroPrelim();
			};
		}

		private static void SubSplashToIntroPrelim() {
			if(!globalObj.canClickOnSplash) {
				return;
			}

			GameObject proxyCamGO = GameObject.Find("ProxyCam");

			RectTransformRotateAnim rectTransformRotateAnim = proxyCamGO.GetComponent<RectTransformRotateAnim>();

			rectTransformRotateAnim.animPostStartDelegate += () => {
				AudioCentralControl.globalObj.PlayAudio("SplashWhooshOut");
			};

			rectTransformRotateAnim.animEndDelegate += SubSplashToIntroFadeTransitionPrelim;

			_ = globalObj.StartCoroutine(globalObj.MyFunc(proxyCamGO, rectTransformRotateAnim));
		}

		private IEnumerator MyFunc(GameObject proxyCamGO, RectTransformRotateAnim rectTransformRotateAnim) {
			yield return new WaitForSeconds(4.0f);

			if(rectTransformRotateAnim != null) {
				rectTransformRotateAnim.IsUpdating = true;
			}
			if(proxyCamGO != null) {
				proxyCamGO.GetComponents<RectTransformScaleAnim>()[1].IsUpdating = true;
			}
		}

		private static void SubSplashToIntroFadeTransitionPrelim() {
			globalObj.canClickOnSplash = false;

			AudioCentralControl.globalObj.PlayAudio("BGM");

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