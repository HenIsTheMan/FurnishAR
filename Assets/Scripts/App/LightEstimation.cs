using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

namespace FurnishAR.App {
    [RequireComponent(typeof(Light))]
    internal sealed class LightEstimation: MonoBehaviour {
        #region Fields
        
        [SerializeField]
        private Light myLight;

        [SerializeField]
        private ARCameraManager camManager;

        [SerializeField]
        private Transform arrowTransform;

        #endregion

        #region Properties

        public float? brightness {
            get;
            private set;
        }

        public float? colorTemperature {
            get;
            private set;
        }

        public Color? colorCorrection {
            get;
            private set;
        }

        public Vector3? mainLightDirection {
            get;
            private set;
        }

        public Color? mainLightColor {
            get;
            private set;
        }

        public float? mainLightIntensityLumens {
            get;
            private set;
        }

        public SphericalHarmonicsL2? sphericalHarmonics {
            get;
            private set;
        }

        #endregion

        #region Ctors and Dtor

        internal LightEstimation(): base() {
            myLight = null;

            camManager = null;

            arrowTransform = null;
        }

        static LightEstimation() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            if(camManager != null)
                camManager.frameReceived += FrameChanged;

            // Disable the arrow to start; enable it later if we get directional light info
            if(arrowTransform) {
                arrowTransform.gameObject.SetActive(false);
            }
            Application.onBeforeRender += OnBeforeRender;
        }

        private void OnDisable() {
            Application.onBeforeRender -= OnBeforeRender;

            if(camManager != null)
                camManager.frameReceived -= FrameChanged;
        }

        #endregion

        private void OnBeforeRender() {
            if(arrowTransform && camManager) {
                var cameraTransform = camManager.GetComponent<Camera>().transform;
                arrowTransform.position = cameraTransform.position + cameraTransform.forward * .25f;
            }
        }

        private void FrameChanged(ARCameraFrameEventArgs args) {
            if(args.lightEstimation.averageBrightness.HasValue) {
                brightness = args.lightEstimation.averageBrightness.Value;
                myLight.intensity = brightness.Value;
            } else {
                brightness = null;
            }

            if(args.lightEstimation.averageColorTemperature.HasValue) {
                colorTemperature = args.lightEstimation.averageColorTemperature.Value;
                myLight.colorTemperature = colorTemperature.Value;
            } else {
                colorTemperature = null;
            }

            if(args.lightEstimation.colorCorrection.HasValue) {
                colorCorrection = args.lightEstimation.colorCorrection.Value;
                myLight.color = colorCorrection.Value;
            } else {
                colorCorrection = null;
            }

            if(args.lightEstimation.mainLightDirection.HasValue) {
                mainLightDirection = args.lightEstimation.mainLightDirection;
                myLight.transform.rotation = Quaternion.LookRotation(mainLightDirection.Value);
                if(arrowTransform) {
                    arrowTransform.gameObject.SetActive(true);
                    arrowTransform.rotation = Quaternion.LookRotation(mainLightDirection.Value);
                }
            } else if(arrowTransform) {
                arrowTransform.gameObject.SetActive(false);
                mainLightDirection = null;
            }

            if(args.lightEstimation.mainLightColor.HasValue) {
                mainLightColor = args.lightEstimation.mainLightColor;
                myLight.color = mainLightColor.Value;
            } else {
                mainLightColor = null;
            }

            if(args.lightEstimation.mainLightIntensityLumens.HasValue) {
                mainLightIntensityLumens = args.lightEstimation.mainLightIntensityLumens;
                myLight.intensity = args.lightEstimation.averageMainLightBrightness.Value;
            } else {
                mainLightIntensityLumens = null;
            }

            if(args.lightEstimation.ambientSphericalHarmonics.HasValue) {
                sphericalHarmonics = args.lightEstimation.ambientSphericalHarmonics;
                RenderSettings.ambientMode = AmbientMode.Skybox;
                RenderSettings.ambientProbe = sphericalHarmonics.Value;
            } else {
                sphericalHarmonics = null;
            }
        }
    }
}