using System.Runtime.InteropServices;
using UnityEngine;
using static IWP.Generic.InitIDs;

namespace IWP.Generic {
	[DisallowMultipleComponent]
	internal sealed class ScreenManager: MonoBehaviour {
		[DllImport("user32.dll", EntryPoint = "FindWindow")] private static extern System.IntPtr FindWindow(string className, string windowName);

		[DllImport("user32.dll", EntryPoint = "SetWindowText")] private static extern bool SetWindowText(System.IntPtr hwnd, string lpString);

		#region Fields

		[SerializeField]
		private InitControl initControl;

		[SerializeField]
		private ScreenModes.ScreenMode mode;

		[Min(1), SerializeField]
		private int width;

		[Min(1), SerializeField]
		private int height;

		[SerializeField]
		private int preferredRefreshRate;

		private string oldWindowTitle;

		[SerializeField]
		private string newWindowTitle;

		private static ScreenManager globalObj;

		#endregion

		#region Properties

		internal static float ScreenWidth {
			get {
				return globalObj.width;
			}
		}

		internal static float ScreenHeight {
			get {
				return globalObj.height;
			}
		}

		#endregion

		#region Ctors and Dtor

		internal ScreenManager(): base() {
			initControl = null;

			mode = ScreenModes.ScreenMode.Windowed;
			width = 1;
			height = 1;
			preferredRefreshRate = 0;

			oldWindowTitle = string.Empty;
			newWindowTitle = string.Empty;
		}

		static ScreenManager() {
			globalObj = null;
		}

		#endregion

		#region Unity User Callback Event Funcs

		private void OnValidate() {
			UnityEngine.Assertions.Assert.AreNotEqual(
				mode, ScreenModes.ScreenMode.Amt,
				"mode, ScreenModes.ScreenMode.Amt"
			);
		}

		private void OnEnable() {
			initControl.AddMethod((uint)InitID.ScreenManager, Init);
		}

		private void OnDisable() {
			initControl.RemoveMethod((uint)InitID.ScreenManager, Init);
		}

		#endregion

		private void Init() {
			globalObj = this;

			oldWindowTitle = Application.productName;

			if(Screen.fullScreenMode != (FullScreenMode)mode || Screen.width != width || Screen.height != height) {
				SetScreenRes(width, height, mode, preferredRefreshRate);
			}

			if(mode == ScreenModes.ScreenMode.Windowed && newWindowTitle != oldWindowTitle) {
				ISetWindowText(FindWindow(null, oldWindowTitle), ref newWindowTitle);
			}
		}

		internal void SetScreenRes(int width, int height, ScreenModes.ScreenMode mode, int preferredRefreshRate = 0) {
			Screen.SetResolution(width, height, (FullScreenMode)mode, preferredRefreshRate);
		}

		internal void ISetWindowText(System.IntPtr windowHandle, ref string windowTitle) {
			_ = SetWindowText(windowHandle, windowTitle);
		}
	}
}