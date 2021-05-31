using IWP.Math;
using UnityEngine;

namespace IWP.Anim {
    internal sealed class TransformRotateAnim: AbstractRotateAnim {
		#region Fields

		[HideInInspector, SerializeField]
		internal Transform myTransform;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal TransformRotateAnim(): base() {
			myTransform = null;
		}

        static TransformRotateAnim() {
        }

        #endregion

        #region Unity User Callback Event Funcs
		#endregion

		protected override void SubInitStuff() {
			myTransform.localRotation = Quaternion.Euler(startEulerAngles);
		}

		protected override void UpdateAnim() {
			myEulerAngles = Val.Lerp(ref startEulerAngles, ref endEulerAngles, easingDelegate(x: Mathf.Min(1.0f, animTime / animDuration)));
			myTransform.localRotation = Quaternion.Euler(myEulerAngles);
		}
	}
}