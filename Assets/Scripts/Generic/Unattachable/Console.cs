using System.Reflection;

namespace FurnishAR.Generic {
	internal static class Console: object {
		private static readonly MethodInfo clearMethodInfo;

		internal delegate void ClearConsoleDelegate();
		internal static ClearConsoleDelegate clearConsoleDelegate;

		static Console() {
			if(PlatDetect.IsInUnityEditor) {
				clearMethodInfo = System.Type.GetType("UnityEditor.LogEntries, UnityEditor", false).GetMethod("Clear");
			}
		}

		internal static void Log(object msg) {
			UnityEngine.Debug.Log(msg);
		}

		internal static void LogWarning(object msg) {
			UnityEngine.Debug.LogWarning(msg);
		}

		internal static void LogError(object msg) {
			UnityEngine.Debug.LogError(msg);
		}

		internal static void Clear() {
			_ = clearMethodInfo?.Invoke(null, null);
			clearConsoleDelegate?.Invoke();
		}
	}
}