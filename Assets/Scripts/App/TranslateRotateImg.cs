using FurnishAR.Generic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class TranslateRotateImg: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        [SerializeField]
        private Transform camTransform;

        [SerializeField]
        private FurnitureManager furnitureManager;

        [SerializeField]
        private float translationSensX;

        [SerializeField]
        private float translationSensZ;

        [SerializeField]
        private float rotationSens;

        private Stack<Vector3> translations;
        private Stack<float> angles;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal TranslateRotateImg() : base() {
            initControl = null;

            camTransform = null;

            furnitureManager = null;

            translationSensX = 1.0f;
            translationSensZ = 1.0f;

            rotationSens = 1.0f;

            translations = null;
            angles = null;
        }

        static TranslateRotateImg() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.TranslateRotateImg, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.TranslateRotateImg, Init);
        }

        #endregion

        private void Init() {
            EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry dragEntry = new EventTrigger.Entry {
                eventID = EventTriggerType.Drag
            };
            dragEntry.callback.AddListener((eventData) => {
                OnDragHandler((PointerEventData)eventData);
            });

            EventTrigger.Entry endDragEntry = new EventTrigger.Entry {
                eventID = EventTriggerType.EndDrag
            };
            endDragEntry.callback.AddListener((eventData) => {
                OnEndDragHandler((PointerEventData)eventData);
            });

            eventTrigger.triggers.Add(dragEntry);
            eventTrigger.triggers.Add(endDragEntry);
        }

        private void OnDragHandler(PointerEventData ptrEventData) {
            if(Input.touchCount == 2) {
                furnitureManager.SelectedFurnitureGO.transform.localRotation
                    = Quaternion.AngleAxis(-ptrEventData.delta.x * rotationSens, Vector3.up)
                    * furnitureManager.SelectedFurnitureGO.transform.localRotation;
            } else if(Input.touchCount == 1) {
                Vector3 front = furnitureManager.SelectedFurnitureGO.transform.localPosition - camTransform.localPosition;
                front.y = 0.0f;

                furnitureManager.SelectedFurnitureGO.transform.localPosition
                    += ptrEventData.delta.x * translationSensX * Vector3.Cross(Vector3.up, front)
                    + ptrEventData.delta.y * translationSensZ * front;
            }
        }

        private void OnEndDragHandler(PointerEventData ptrEventData) {
            if(Input.touchCount == 2) {
                angles.Push(-ptrEventData.delta.x * rotationSens);
            } else if(Input.touchCount == 1) {
                Vector3 front = furnitureManager.SelectedFurnitureGO.transform.localPosition - camTransform.localPosition;
                front.y = 0.0f;

                translations.Push(ptrEventData.delta.x * translationSensX * Vector3.Cross(Vector3.up, front)
                    + ptrEventData.delta.y * translationSensZ * front);
            }
        }
    }
}