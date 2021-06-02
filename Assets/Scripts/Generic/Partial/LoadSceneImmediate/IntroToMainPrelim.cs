using UnityEngine;
using UnityEngine.XR.Management;

namespace FurnishAR.Generic {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		public static void IntroToMainPrelim() {
			var xrManagerSettings = XRGeneralSettings.Instance.Manager;
			xrManagerSettings.DeinitializeLoader();
		}
	}
}