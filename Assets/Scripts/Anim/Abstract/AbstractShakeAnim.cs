using UnityEngine;

namespace FurnishAR.Anim {
    internal abstract class AbstractShakeAnim: AbstractAnim {
		#region Fields

		protected float val;
		protected int prevVal;

		protected Vector3 pos0;
		protected Vector3 pos1;

		[HideInInspector, SerializeField]
		internal Vector3 startPos;

		[HideInInspector, SerializeField]
		internal Vector3 minOffset;

		[HideInInspector, SerializeField]
		internal Vector3 maxOffset;

		[HideInInspector, Min(2), SerializeField]
		internal int maxMoveCount;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal AbstractShakeAnim(): base() {
			val = 0.0f;
			prevVal = 0;

			pos0 = Vector3.zero;
			pos1 = Vector3.zero;

			startPos = Vector3.zero;
			minOffset = Vector3.zero;
			maxOffset = Vector3.zero;

			maxMoveCount = 2;
		}

        static AbstractShakeAnim() {
        }

		#endregion

		#region Unity User Callback Event Funcs
		#endregion

		protected override void ResetMe() {
			base.ResetMe();

			prevVal = 0;
		}
    }
}