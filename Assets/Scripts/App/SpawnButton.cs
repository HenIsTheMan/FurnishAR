using UnityEngine;

namespace FurnishAR.App {
    internal sealed class SpawnButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private PlacementMarkerControl placementMarkerControl;

        public GameObject furniture;

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
            Generic.Console.Log("here");


            Transform furnitureTransform = furniture.transform;
            furnitureTransform.gameObject.SetActive(true);

            Vector3 pos = placementMarkerControl.PlacementMarkerParentTransform.position;
            //pos.y += 4.0f;
            furnitureTransform.position = pos;

            placementMarkerControl.PlacementMarkerParentTransform.gameObject.SetActive(false);
        }
    }
}