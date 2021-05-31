#if UNITY_EDITOR

using UnityEditor;

namespace FurnishAR.Anim {
	[CustomEditor(typeof(TransformPathAnim)), CanEditMultipleObjects]
	internal sealed class TransformPathAnimEditor: AbstractAnimEditor {
		#region Fields
		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal TransformPathAnimEditor(): base() {
		}

		static TransformPathAnimEditor() {
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
				"myTransform",
				"wayptTransforms"
			};
		}
	}
}

#endif