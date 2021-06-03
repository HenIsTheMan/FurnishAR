using FurnishAR.Anim;
using FurnishAR.Generic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class PlacementMarkerControl: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        private bool shldReset;

        private List<ARRaycastHit> hits;

        [SerializeField]
        private ARRaycastManager raycastManager;

        [SerializeField]
        private float rotationSensX;

        [SerializeField]
        private float rotationSensY;

        [SerializeField]
        private Transform placementMarkerParentTransform;

        private GameObject placementMarkerGO;

        private Vector3 translateOG;
        private Quaternion rotateOG;
        private Vector3 scaleOG;

        [SerializeField]
        private CanvasGrpFadeAnim nthToResetFadeAnim;

        [SerializeField]
        private RectTransformTranslateAnim nthToResetTranslateAnim;

        #endregion

        #region Properties

        internal GameObject PlacementMarkerGO {
            get => placementMarkerGO;
        }

        #endregion

        #region Ctors and Dtor

        internal PlacementMarkerControl(): base() {
            initControl = null;

            shldReset = false;

            hits = null;

            raycastManager = null;

            rotationSensX = 1.0f;
            rotationSensY = 1.0f;

            placementMarkerParentTransform = null;

            placementMarkerGO = null;

            translateOG = Vector3.zero;
            rotateOG = Quaternion.identity;
            scaleOG = Vector3.one;

            nthToResetFadeAnim = null;
            nthToResetTranslateAnim = null;
        }

        static PlacementMarkerControl() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.PlacementMarkerControl, Init);
        }

        private void Update() {
            if(placementMarkerGO == null) {
                return;
            }

            raycastManager.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), hits, TrackableType.Planes);

            if(hits.Count > 0) {
                placementMarkerParentTransform.localPosition = hits[0].pose.position;
                placementMarkerParentTransform.localRotation = hits[0].pose.rotation;
                placementMarkerGO.SetActive(true);
            } else {
                placementMarkerGO.SetActive(false);
            }
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.PlacementMarkerControl, Init);
        }

        #endregion

        private void Init() {
            hits = new List<ARRaycastHit>();

            EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry dragEntry = new EventTrigger.Entry {
                eventID = EventTriggerType.Drag
            };
            dragEntry.callback.AddListener((eventData) => {
                OnDragHandler((PointerEventData)eventData);
            });

            eventTrigger.triggers.Add(dragEntry);

            nthToResetFadeAnim.gameObject.GetComponent<CanvasGroup>().alpha = 0.0f; //Workaround
        }

        private void OnDragHandler(PointerEventData ptrEventData) {
            if(Input.touchCount == 2) {
                shldReset = true;

                placementMarkerGO.transform.localRotation
                    = Quaternion.AngleAxis(-ptrEventData.delta.x * rotationSensY, Vector3.up)
                    * Quaternion.AngleAxis(ptrEventData.delta.y * rotationSensX, Vector3.right)
                    * placementMarkerGO.transform.localRotation;
            }
        }

        internal void ConfigPlacementMarkerGO(GameObject GO, ref Vector3 translate, ref Quaternion rotate, ref Vector3 scale) {
            placementMarkerGO = GO;
            placementMarkerGO.SetActive(false);

            placementMarkerGO.transform.localPosition = translateOG = translate;
            placementMarkerGO.transform.localRotation = rotateOG = rotate;
            placementMarkerGO.transform.localScale = scaleOG = scale;
        }

        public void ResetTransformOfPlacementMarkerGO() {
            if(placementMarkerGO != null) {
                if(shldReset) {
                    placementMarkerGO.transform.localPosition = translateOG;
                    placementMarkerGO.transform.localRotation = rotateOG;
                    placementMarkerGO.transform.localScale = scaleOG;

                    shldReset = false;
                } else {
                    nthToResetFadeAnim.IsUpdating = false;
                    nthToResetTranslateAnim.IsUpdating = false;

                    nthToResetFadeAnim.IsUpdating = true;
                    nthToResetTranslateAnim.IsUpdating = true;
                }
            }
        }
    }
}