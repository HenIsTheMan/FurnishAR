using UnityEngine;

namespace FurnishAR.App {
    internal sealed class ObjSpawner: MonoBehaviour {
        #region Fields

        [SerializeField]
        private PlacementMarkerControl placementMarkerControl;

        [SerializeField]
        private GameObject prefab;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal ObjSpawner(): base() {
            placementMarkerControl = null;

            prefab = null;
        }

        static ObjSpawner() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Update() {
            if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
                _ = Instantiate(prefab, placementMarkerControl.transform.position, placementMarkerControl.transform.rotation);
            }
        }

        #endregion
    }
}