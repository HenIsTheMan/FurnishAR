using TMPro;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class EllipsesControl: MonoBehaviour {
        #region Fields

        private float BT;
        private float elapsedTime;

        [SerializeField]
        private float delay;

        [SerializeField]
        private TextMeshProUGUI tmpComponent;

        private int dotCount;

        #endregion

        #region Properties

        internal int DotCount {
            get {
                return dotCount;
            }
        }

        internal TextMeshProUGUI TmpComponent {
            get {
                return tmpComponent;
            }
        }

        #endregion

        #region Ctors and Dtor

        internal EllipsesControl() {
            BT = 0.0f;
            elapsedTime = 0.0f;
            delay = 0.0f;
            tmpComponent = null;
            dotCount = 0;
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            UnityEngine.Assertions.Assert.IsNotNull(tmpComponent);
        }

        private void Update() {
            elapsedTime += Time.deltaTime;

            if(tmpComponent.text.Length > 0 && BT <= elapsedTime) {
                while(tmpComponent.text.Substring(tmpComponent.text.Length - 1) == ".") {
                    tmpComponent.text = tmpComponent.text.Substring(0, tmpComponent.text.Length - 1);
                }

                dotCount = dotCount == 3 ? 0 : dotCount + 1;
                for(int i = 0; i < dotCount; ++i) {
                    tmpComponent.text += '.';
                }

                BT = elapsedTime + delay;
            }
        }

        #endregion
    }
}