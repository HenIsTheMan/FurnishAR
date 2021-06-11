using FurnishAR.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class SwipeDownDetector: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        [SerializeField]
        private ThinArrowButton thinDownArrowButton;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal SwipeDownDetector(): base() {
            initControl = null;
            thinDownArrowButton = null;
        }

        static SwipeDownDetector() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.SwipeDownDetector, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.SwipeDownDetector, Init);
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

            eventTrigger.triggers.Add(dragEntry);
        }

        private void OnDragHandler(PointerEventData ptrEventData) {
            if(ptrEventData.delta.y < 0.0f) {
                thinDownArrowButton.OnClick();
            }
        }
    }
}