using FurnishAR.Math;
using TMPro;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class ScanningText: MonoBehaviour {
        #region Fields

        private bool isFlashing;

        private Color myColor;

        private float currTime;
        private float flashTime;

        private float lerpFactor;

        [SerializeField]
        private Vector3 startColor;

        [SerializeField]
        private Vector3 endColor;

        [SerializeField]
        private float startAlpha;

        [SerializeField]
        private float endAlpha;

        [SerializeField]
        private TMP_Text text;

        #endregion

        #region Properties
        #endregion
        
        #region Ctors and Dtor

        internal ScanningText(): base() {
            isFlashing = false;

            myColor = Color.white;

            currTime = 0.0f;
            flashTime = 0.0f;

            lerpFactor = 0.0f;

            startColor = Vector3.one;
            endColor = Vector3.one;

            startAlpha = 1.0f;
            endAlpha = 1.0f;

            text = null;
        }

        static ScanningText() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Update() {
            if(!isFlashing) {
                return;
            }

            currTime -= Time.deltaTime;

            if(currTime <= flashTime) {
                lerpFactor = Mathf.Sin(currTime * 4.0f * (flashTime - currTime)) * 0.5f + 0.5f;

                myColor.r = Val.Lerp(startColor.x, endColor.x, lerpFactor);
                myColor.g = Val.Lerp(startColor.y, endColor.y, lerpFactor);
                myColor.b = Val.Lerp(startColor.z, endColor.z, lerpFactor);
                myColor.a = Val.Lerp(startAlpha, endAlpha, lerpFactor);

                text.color = myColor;
            } else {
                text.color = new Color(startColor.x, startColor.y, startColor.z, startAlpha);
                isFlashing = false;
            }
        }

        #endregion

        internal void StartFlashing(float flashTime) {
            currTime = this.flashTime = flashTime;
            isFlashing = true;
        }
    }
}