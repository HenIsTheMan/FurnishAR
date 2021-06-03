using UnityEngine;
using UnityEngine.XR.Management;

namespace FurnishAR.Generic {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		public static void IntroToMainPrelim() {
			var XRManagerSettings = XRGeneralSettings.Instance.Manager;
			XRManagerSettings.DeinitializeLoader();
		}
	}
}