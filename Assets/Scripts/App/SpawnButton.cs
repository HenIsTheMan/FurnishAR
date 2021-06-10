using UnityEngine;

namespace FurnishAR.App {
    internal sealed class SpawnButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private PlacementMarkerControl placementMarkerControl;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal SpawnButton(): base() {
            placementMarkerControl = null;
        }

        static SpawnButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            Transform furnitureTransform = GameObject.Find("BlackLeatherChair").transform;
            furnitureTransform.gameObject.SetActive(true);

            Vector3 pos = placementMarkerControl.PlacementMarkerParentTransform.localPosition;
            pos.y += 4.0f;
            furnitureTransform.localPosition = pos;

            placementMarkerControl.PlacementMarkerParentTransform.gameObject.SetActive(false);
        }
    }
}