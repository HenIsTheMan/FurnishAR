using FurnishAR.Math;
using UnityEngine;

namespace FurnishAR.Anim {
    internal sealed class RectTransformTranslateAnim: AbstractTranslateAnim {
		#region Fields

		[HideInInspector, SerializeField]
		internal RectTransform myRectTransform;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal RectTransformTranslateAnim(): base() {
			myRectTransform = null;
		}

        static RectTransformTranslateAnim() {
        }

        #endregion

        #region Unity User Callback Event Funcs
		#endregion

		protected override void SubInitStuff() {
			Generic.Console.Log("here0");

			myRectTransform.localPosition = startPos;
		}

		protected override void UpdateAnim() {
			myRectTransform.localPosition = Val.Lerp(ref startPos, ref endPos, easingDelegate(x: Mathf.Min(1.0f, animTime / animDuration)));
		}
	}
}