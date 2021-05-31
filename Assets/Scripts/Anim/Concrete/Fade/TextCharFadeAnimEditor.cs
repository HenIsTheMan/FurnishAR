#if UNITY_EDITOR

using UnityEditor;

namespace FurnishAR.Anim {
	[CustomEditor(typeof(TextCharFadeAnim)), CanEditMultipleObjects]
	internal sealed class TextCharFadeAnimEditor: AbstractAnimEditor {
		#region Fields
		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal TextCharFadeAnimEditor(): base() {
		}

		static TextCharFadeAnimEditor() {
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
				"tmpComponent"
			};
		}
	}
}

#endif