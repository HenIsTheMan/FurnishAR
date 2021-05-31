using UnityEngine;

namespace IWP.Anim {
	internal abstract class AbstractTranslateAnim: AbstractAnim {
		#region Fields

		[HideInInspector, SerializeField]
		internal Vector3 startPos;

		[HideInInspector, SerializeField]
		internal Vector3 endPos;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal AbstractTranslateAnim(): base() {
			startPos = Vector3.zero;
			endPos = Vector3.zero;
		}

        static AbstractTranslateAnim() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion
    }
}