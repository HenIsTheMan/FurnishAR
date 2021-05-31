#if UNITY_EDITOR

using UnityEditor;

namespace IWP.Anim {
	[CustomEditor(typeof(TransformShakeAnim)), CanEditMultipleObjects]
	internal sealed class TransformShakeAnimEditor: AbstractAnimEditor {
		#region Fields
		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal TransformShakeAnimEditor(): base() {
		}

		static TransformShakeAnimEditor() {
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
				"minOffset",
				"maxOffset",
				"maxMoveCount",
				"myTransform"
			};
		}
	}
}

#endif