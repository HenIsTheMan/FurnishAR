using IWP.Anim;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace IWP.Generic {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		public static void SplashToIntroPrelim() {
			GameObject.Find("GenesisAlpha0").GetComponent<RectTransformPathAnim>().animEndDelegate += SubSplashToIntroPrelim;
		}

		private static void SubSplashToIntroPrelim() {
			if(!globalObj.canClickOnSplash) {
				return;
			}

			BloomAnim[] bloomAnims = GameObject.Find("BloomVol").GetComponents<BloomAnim>();
			bloomAnims[0].IsUpdating = true;
			bloomAnims[1].IsUpdating = true;

			bloomAnims[1].animPrePeriodicDelegate += () => {
				if(UnityEngine.SceneManagement.SceneManager.GetSceneByName("IntroScene").isLoaded) {
					bloomAnims[0].IsUpdating = false;
					bloomAnims[1].IsUpdating = false;
				}
			};

			bloomAnims[1].animEndDelegate = () => {
				if(!globalObj.canClickOnSplash) {
					return;
				}

				GameObject proxyCamGO = GameObject.Find("ProxyCam");

				RectTransformRotateAnim rectTransformRotateAnim = proxyCamGO.GetComponent<RectTransformRotateAnim>();

				rectTransformRotateAnim.IsUpdating = true;
				proxyCamGO.GetComponents<RectTransformScaleAnim>()[1].IsUpdating = true;

				rectTransformRotateAnim.animEndDelegate += SubSplashToIntroFadeTransitionPrelim;
			};
		}

		private static void SubSplashToIntroFadeTransitionPrelim() {
			globalObj.canClickOnSplash = false;

			PtrManager ptrManager = FindObjectOfType<PtrManager>();
			ptrManager.ChangeCursor("HexCursor", Vector2.zero, CursorModes.CursorMode.Auto);
			ptrManager.DeactivateAllPtrTrails();
			ptrManager.ActivatePtrTrail("HexPtrTrail");

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