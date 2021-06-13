using FurnishAR.Generic;
using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class AudioCentralControl: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        private AudioSource[] audioSrcs;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal AudioCentralControl(): base() {
            initControl = null;

            audioSrcs = System.Array.Empty<AudioSource>();
        }

        static AudioCentralControl() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.AudioCentralControl, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.AudioCentralControl, Init);
        }

        #endregion

        private void Init() {
            audioSrcs = FindObjectsOfType<AudioSource>();
        }

        internal void PauseAllAudio() {
            foreach(AudioSource audioSrc in audioSrcs) {
                audioSrc.Pause();
            }
        }

        internal void PlayAllAudio() {
            foreach(AudioSource audioSrc in audioSrcs) {
                audioSrc.Play();
            }
        }

        internal void StopAllAudio() {
            foreach(AudioSource audioSrc in audioSrcs) {
                audioSrc.Stop();
            }
        }

        internal void PlayAudio(string name) {
            StopAllAudio();

            foreach(AudioSource audioSrc in audioSrcs) {
                if(audioSrc.gameObject.name == name) {
                    audioSrc.Play();
                    break;
                }
            }
        }
    }
}