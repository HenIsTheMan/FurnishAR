#if UNITY_EDITOR

using UnityEditor;

namespace FurnishAR.Anim {
	[CustomEditor(typeof(RectTransformTranslateAnim)), CanEditMultipleObjects]
	internal sealed class RectTransformTranslateAnimEditor: AbstractAnimEditor {
		#region Fields
		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal RectTransformTranslateAnimEditor(): base() {
		}

		static RectTransformTranslateAnimEditor() {
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