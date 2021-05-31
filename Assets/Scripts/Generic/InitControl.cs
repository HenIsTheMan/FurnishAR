using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.Generic {
	[DisallowMultipleComponent]
	internal sealed class InitControl: MonoBehaviour {
		#region Fields

		internal delegate void InitDelegate();

		private uint size;
		private InitDelegate[] initDelegates;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal InitControl(): base() {
			size = (uint)InitID.Amt;

			initDelegates = new InitDelegate[size];

			for(uint i = 0; i < size; ++i) {
				initDelegates[i] = null;
			}
		}

		static InitControl() {
		}

		#endregion

		#region Unity User Callback Event Funcs

		private void Start() {
			for(uint i = 0; i < size; ++i) {
				initDelegates[i]?.Invoke();
			}
		}

		#endregion

		internal void AddMethod(uint i, InitDelegate initDelegate) {
			initDelegates[i] += initDelegate;
		}

		internal void RemoveMethod(uint i, InitDelegate initDelegate) {
			initDelegates[i] -= initDelegate;
		}
	}
}