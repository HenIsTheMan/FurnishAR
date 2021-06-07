using TMPro;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class AcctManager: MonoBehaviour {
        #region Fields

        [SerializeField]
        internal TMP_Text bigInfoLabel;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal AcctManager(): base() {
            bigInfoLabel = null;
        }

        static AcctManager() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion
    }
}