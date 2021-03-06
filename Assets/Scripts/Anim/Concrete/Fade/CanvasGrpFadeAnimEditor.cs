#if UNITY_EDITOR

using UnityEditor;

namespace FurnishAR.Anim {
	[CustomEditor(typeof(CanvasGrpFadeAnim)), CanEditMultipleObjects]
	internal sealed class CanvasGrpFadeAnimEditor: AbstractAnimEditor {
		#region Fields
		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal CanvasGrpFadeAnimEditor(): base() {
		}

		static CanvasGrpFadeAnimEditor() {
		}

		#endregion

		#region Unity User Callback Event Funcs
		#endregion

		protected override void InitNames() {
			names = new string[]{
				"initControl",
				"isUpdating",
				"periodicDelay",
				"startTimeOffset",
				"animDuration",
				"countThreshold",
				"easingType",
				"startAlpha",
				"endAlpha",
				"canvasGrp"
			};
		}
	}
}

#endif