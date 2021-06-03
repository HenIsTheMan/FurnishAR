using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace FurnishAR.Generic {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		public static void IntroToMainPrelim() {
			LoaderUtility.Initialize();
		}
	}
}