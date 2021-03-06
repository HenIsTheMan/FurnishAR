using FurnishAR.Anim;
using FurnishAR.Generic;
using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class MainPanel: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        [SerializeField]
        private GameObject browseScrollWheelGO;

        [SerializeField]
        private GameObject savedScrollWheelGO;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal MainPanel(): base() {
            initControl = null;

            browseScrollWheelGO = null;
            savedScrollWheelGO = null;
        }

        static MainPanel() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.MainPanel, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.MainPanel, Init);
        }

        #endregion

        private void Init() {
            ((RectTransform)transform).anchoredPosition = new Vector3(0.0f, -2000.0f, 0.0f); //Workaround

            RectTransformAnchoredTranslateAnim[] anims = GetComponents<RectTransformAnchoredTranslateAnim>();
            float offset = 1920.0f - Screen.width;

            anims[0].startPos.y -= offset;
            anims[0].endPos.y -= offset;

            anims[1].startPos.y -= offset;
            anims[1].endPos.y -= offset;

            RectTransform browseScrollViewRectTransform = browseScrollWheelGO.transform as RectTransform;
            RectTransform savedScrollViewRectTransform = savedScrollWheelGO.transform as RectTransform;
            Vector3 pos;

            browseScrollViewRectTransform.sizeDelta = new Vector2(
                browseScrollViewRectTransform.sizeDelta.x,
                (browseScrollViewRectTransform.sizeDelta.y * browseScrollViewRectTransform.localScale.y - offset)
                / browseScrollViewRectTransform.localScale.y
            );

            pos = browseScrollViewRectTransform.localPosition;
            pos.y += offset * 0.5f;
            browseScrollViewRectTransform.localPosition = pos;

            savedScrollViewRectTransform.sizeDelta = new Vector2(
                savedScrollViewRectTransform.sizeDelta.x,
                (savedScrollViewRectTransform.sizeDelta.y * savedScrollViewRectTransform.localScale.y - offset)
                / savedScrollViewRectTransform.localScale.y
            );

            pos = savedScrollViewRectTransform.localPosition;
            pos.y += offset * 0.5f;
            savedScrollViewRectTransform.localPosition = pos;
        }
    }
}