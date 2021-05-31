using IWP.Math;
using UnityEngine;

namespace IWP.Anim {
    internal sealed class RectTransformShakeAnim: AbstractShakeAnim {
		#region Fields

		[HideInInspector, SerializeField]
		internal RectTransform myRectTransform;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal RectTransformShakeAnim(): base() {
			myRectTransform = null;
		}

        static RectTransformShakeAnim() {
        }

        #endregion

        #region Unity User Callback Event Funcs
		#endregion

		protected override void SubInitStuff() {
			myRectTransform.localPosition = startPos;

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

				pos0 = myRectTransform.localPosition;

				prevVal = (int)val;
			}

			if(val >= maxMoveCount - 1) {
				myRectTransform.localPosition = Val.Lerp(ref pos0, ref startPos, easingDelegate(x: val - Mathf.Floor(val)));
			} else {
				myRectTransform.localPosition = Val.Lerp(ref pos0, ref pos1, easingDelegate(x: val - Mathf.Floor(val)));
			}
		}
	}
}