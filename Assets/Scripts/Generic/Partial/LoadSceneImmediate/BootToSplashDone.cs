using UnityEngine;

namespace FurnishAR.Generic {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		public static void BootToSplashDone() {
			PtrManager ptrManager = FindObjectOfType<PtrManager>();
			ptrManager.displacementFromCam = 5.0f;
			ptrManager.camComponent = FindObjectOfType<Camera>();
			ptrManager.ChangeCursorCentered("GenesisCursor", CursorModes.CursorMode.Auto);
		}
	}
}