using FurnishAR.Generic;
using System.IO;
using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class ShareButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        [SerializeField]
        private Camera screenshotCam;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal ShareButton(): base() {
            initControl = null;

            screenshotCam = null;
        }

        static ShareButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.ShareButton, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.ShareButton, Init);
        }

        #endregion

        private void Init() {
            screenshotCam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        }

        public void OnClick() {
            AudioCentralControl.globalObj.PlayAudio("ButtonPress");

            _ = StartCoroutine(TakeScreenshotAndShare());
        }

        private System.Collections.IEnumerator TakeScreenshotAndShare() {
            yield return new WaitForEndOfFrame();

            RenderTexture currRenderTex = RenderTexture.active;

            RenderTexture.active = screenshotCam.targetTexture;
            screenshotCam.Render();

            Texture2D tex2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            tex2D.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            tex2D.Apply();

            RenderTexture.active = currRenderTex;

            string path = Path.Combine(Application.temporaryCachePath, "screenshotForShare.png");
            File.WriteAllBytes(path, tex2D.EncodeToPNG());

            Destroy(tex2D);

            new NativeShare()
                .AddFile(path)
                .SetSubject("Screenshot")
                .SetText("Share this screenshot with others!")
                .Share();
        }
    }
}