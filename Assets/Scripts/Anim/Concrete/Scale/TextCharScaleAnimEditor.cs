#if UNITY_EDITOR

using UnityEditor;

namespace IWP.Anim {
	[CustomEditor(typeof(TextCharScaleAnim)), CanEditMultipleObjects]
	internal sealed class TextCharScaleAnimEditor: AbstractAnimEditor {
		#region Fields
		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal TextCharScaleAnimEditor(): base() {
		}

		static TextCharScaleAnimEditor() {
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
				"startScale",
				"endScale",
				"tmpTextComponent"
			};
		}
	}
}

#endif