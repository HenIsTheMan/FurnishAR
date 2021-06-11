#if UNITY_EDITOR

using UnityEditor;

namespace FurnishAR.Anim {
	[CustomEditor(typeof(RectTransformAnchoredTranslateAnim)), CanEditMultipleObjects]
	internal sealed class RectTransformAnchoredTranslateAnimEditor: AbstractAnimEditor {
		#region Fields
		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal RectTransformAnchoredTranslateAnimEditor(): base() {
		}

		static RectTransformAnchoredTranslateAnimEditor() {
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
				"startPos",
				"endPos",
				"myRectTransform"
			};
		}
	}
}

#endif