using FurnishAR.Math;
using UnityEngine;

namespace FurnishAR.Anim {
    internal sealed class TransformShakeAnim: AbstractShakeAnim {
		#region Fields

		[HideInInspector, SerializeField]
		internal Transform myTransform;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal TransformShakeAnim(): base() {
			myTransform = null;
		}

        static TransformShakeAnim() {
        }

        #endregion

        #region Unity User Callback Event Funcs
		#endregion

		protected override void SubInitStuff() {
			myTransform.localPosition = startPos;

			pos1.x = startPos.x + Random.Range(minOffset.x, maxOffset.x);
			pos1.y = startPos.y + Random.Range(minOffset.y, maxOffset.y);
			pos1.z = startPos.z + Random.Range(minOffset.z, maxOffset.z);

			pos0 = startPos;
		}

		protected override void UpdateAnim() {
			val = Val.Lerp(0.0f, maxMoveCount, Mathf.Min(1.0f, animTime / animDuration));

			if((int)val != prevVal) {
				pos1.x = startPos.x + Random.Range(minOffset.x, maxOffset.x);
				pos1.y = startPos.y + Random.Range(minOffset.y, maxOffset.y);
				pos1.z = startPos.z + Random.Range(minOffset.z, maxOffset.z);

				pos0 = myTransform.localPosition;

				prevVal = (int)val;
			}

			if(val >= maxMoveCount - 1) {
				myTransform.localPosition = Val.Lerp(ref pos0, ref startPos, easingDelegate(x: val - Mathf.Floor(val)));
			} else {
				myTransform.localPosition = Val.Lerp(ref pos0, ref pos1, easingDelegate(x: val - Mathf.Floor(val)));
			}
		}
	}
}