using UnityEngine;

namespace FurnishAR.App {
    internal sealed class Selection: MonoBehaviour {
        #region Fields

        internal int storedIndex;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal Selection(): base() {
            storedIndex = -1;
        }

        static Selection() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion
    }
}