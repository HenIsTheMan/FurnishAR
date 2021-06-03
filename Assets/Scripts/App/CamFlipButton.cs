using FurnishAR.Generic;
using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.Apps {
    internal sealed class CamFlipButton: MonoBehaviour {
        class PreferCameraConfigurationChooser: ConfigurationChooser {
            public override Configuration ChooseConfiguration(NativeSlice<ConfigurationDescriptor> descriptors, Feature requestedFeatures) {
                if(descriptors.Length == 0) {
                    throw new ArgumentException("No configuration descriptors to choose from.", nameof(descriptors));
                }

                if(requestedFeatures.Intersection(Feature.AnyTrackingMode).Count() > 1) {
                    throw new ArgumentException($"Zero or one tracking mode must be requested. Requested tracking modes => {requestedFeatures.Intersection(Feature.AnyTrackingMode).ToStringList()}", nameof(requestedFeatures));
                }

                if(requestedFeatures.Intersection(Feature.AnyCamera).Count() > 1) {
                    throw new ArgumentException($"Zero or one camera mode must be requested. Requested camera modes => {requestedFeatures.Intersection(Feature.AnyCamera).ToStringList()}", nameof(requestedFeatures));
                }

                // Get the requested camera features out of the set of requested features.
                Feature requestedCameraFeatures = requestedFeatures.Intersection(Feature.AnyCamera);

                int highestFeatureWeight = -1;
                int highestRank = int.MinValue;
                ConfigurationDescriptor bestDescriptor = default;
                foreach(var descriptor in descriptors) {
                    // Initialize the feature weight with each feature being an equal weight.
                    int featureWeight = requestedFeatures.Intersection(descriptor.capabilities).Count();

                    // Increase the weight if there are matching camera features.
                    if(requestedCameraFeatures.Any(descriptor.capabilities)) {
                        featureWeight += 100;
                    }

                    // Store the descriptor with the highest feature weight.
                    if((featureWeight > highestFeatureWeight) ||
                        (featureWeight == highestFeatureWeight && descriptor.rank > highestRank)) {
                        highestFeatureWeight = featureWeight;
                        highestRank = descriptor.rank;
                        bestDescriptor = descriptor;
                    }
                }

                // Return the configuration with the best matching descriptor.
                return new Configuration(bestDescriptor, requestedFeatures.Intersection(bestDescriptor.capabilities));
            }
        }

        #region Fields

        [SerializeField]
        private InitControl initControl;

        [SerializeField]
        private ARSession myARSession;

        [SerializeField]
        private ARCameraManager camManager;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal CamFlipButton(): base() {
            initControl = null;

            myARSession = null;

            camManager = null;
        }

        static CamFlipButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.CamFlipButton, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.CamFlipButton, Init);
        }

        #endregion

        private void Init() {
            myARSession.subsystem.configurationChooser = new PreferCameraConfigurationChooser();
        }

        public void FlipCam() {
            camManager.requestedFacingDirection = camManager.requestedFacingDirection == CameraFacingDirection.User
                ? CameraFacingDirection.World
                : CameraFacingDirection.User;
        }
    }
}