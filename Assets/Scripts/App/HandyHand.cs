using FurnishAR.Generic;
using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class HandyHand: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        [SerializeField]
        private Vector3 localPos;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal HandyHand(): base() {
            initControl = null;

            localPos = Vector3.zero;
        }

        static HandyHand() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.HandyHand, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.HandyHand, Init);
        }

        #endregion

        private void Init() {
            transform.localPosition = localPos; //Workaround
        }
    }
}