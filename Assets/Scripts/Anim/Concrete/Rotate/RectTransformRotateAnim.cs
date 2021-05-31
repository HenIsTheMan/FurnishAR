using FurnishAR.Math;
using UnityEngine;

namespace FurnishAR.Anim {
    internal sealed class RectTransformRotateAnim: AbstractRotateAnim {
		#region Fields

		[HideInInspector, SerializeField]
		internal RectTransform myRectTransform;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal RectTransformRotateAnim(): base() {
			myRectTransform = null;
		}

        static RectTransformRotateAnim() {
        }

        #endregion

        #region Unity User Callback Event Funcs
		#endregion

		protected override void SubInitStuff() {
			myRectTransform.localRotation = Quaternion.Euler(startEulerAngles);
		}

		protected override void UpdateAnim() {
			myEulerAngles = Val.Lerp(ref startEulerAngles, ref endEulerAngles, easingDelegate(x: Mathf.Min(1.0f, animTime / animDuration)));
			myRectTransform.localRotation = Quaternion.Euler(myEulerAngles);
		}
	}
}