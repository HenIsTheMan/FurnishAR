using System.IO;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class ShareButton: MonoBehaviour {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal ShareButton(): base() {
        }

        static ShareButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            AudioCentralControl.globalObj.PlayAudio("ButtonPress");

            _ = StartCoroutine(TakeScreenshotAndShare());
        }

        private System.Collections.IEnumerator TakeScreenshotAndShare() {
            yield return new WaitForEndOfFrame();

            Texture2D tex2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            tex2D.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            tex2D.Apply();

            string path = Path.Combine(Application.temporaryCachePath, "screenshotForShare.png");
            File.WriteAllBytes(path, tex2D.EncodeToPNG());

            Destroy(tex2D);

            new NativeShare()
                .AddFile(path)
                .SetSubject("Screenshot")
                .SetText("Share this screenshot others!")
                .Share();
        }
    }
}