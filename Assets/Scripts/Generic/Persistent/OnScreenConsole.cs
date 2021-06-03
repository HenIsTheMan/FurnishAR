using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.Generic {
	[DisallowMultipleComponent]
	internal sealed class OnScreenConsole: MonoBehaviour {
		#region Fields

		[SerializeField]
		private InitControl initControl;

		private Rect rect;
		private string myLog;

		[SerializeField]
		private bool isVisible;

		[SerializeField]
		private KeyCode keyCode;

		[SerializeField]
		private float xOffset;

		[SerializeField]
		private float yOffset;

		[SerializeField]
		private float widthOffset;

		[SerializeField]
		private float heightOffset;

		[SerializeField]
		private Color bgColor;

		[SerializeField]
		private Color contentColor;

		[Min(0), SerializeField]
		private int fontSize;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal OnScreenConsole(): base() {
			initControl = null;

			rect = Rect.zero;
			myLog = string.Empty;
			isVisible = false;
			keyCode = KeyCode.Space;

			xOffset = 0.0f;
			yOffset = 0.0f;
			widthOffset = 0.0f;
			heightOffset = 0.0f;
			bgColor = Color.white;
			contentColor = Color.white;
			fontSize = 0;
		}

		static OnScreenConsole() {
		}

		#endregion

		#region Unity User Callback Event Funcs

		private void OnEnable() {
			initControl.AddMethod((uint)InitID.OnScreenConsole, Init);

			Application.logMessageReceivedThreaded += LogToOnScreenConsole;
			Console.clearConsoleDelegate += ClearOnScreenConsole;
		}

		private void Update() {
			if(Input.GetKeyDown(keyCode)) {
				isVisible = !isVisible;
			}
		}

		private void OnGUI() {
			if(isVisible) {
				GUI.skin.textArea.fontSize = fontSize;
				GUI.backgroundColor = bgColor;
				GUI.contentColor = contentColor;
				GUI.TextArea(rect, myLog);
			}
		}

		private void OnDisable() {
			initControl.RemoveMethod((uint)InitID.OnScreenConsole, Init);

			Application.logMessageReceivedThreaded -= LogToOnScreenConsole;
			Console.clearConsoleDelegate -= ClearOnScreenConsole;
		}

		#endregion

		private void Init() {
			rect = new Rect(xOffset, yOffset, ScreenManager.ScreenWidth + widthOffset, ScreenManager.ScreenHeight + heightOffset);
		}

		private void LogToOnScreenConsole(string msg, string stackTrace, LogType logType) {
			myLog += (int)logType + '\n' + stackTrace + "\nHo" + msg + "\n\n\nHey";
		}

		private void ClearOnScreenConsole() {
			myLog = string.Empty;
		}
	}
}