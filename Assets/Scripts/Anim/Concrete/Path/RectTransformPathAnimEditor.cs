#if UNITY_EDITOR

using UnityEditor;

namespace FurnishAR.Anim {
	[CustomEditor(typeof(RectTransformPathAnim)), CanEditMultipleObjects]
	internal sealed class RectTransformPathAnimEditor: AbstractAnimEditor {
		#region Fields
		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal RectTransformPathAnimEditor(): base() {
		}

		static RectTransformPathAnimEditor() {
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
				"startSpd",
				"endSpd",
				"currWayptIndex",
				"myRectTransform",
				"wayptRectTransforms"
			};
		}
	}
}

#endif