using UnityEngine;

namespace FurnishAR.App {
    internal sealed class AudioCentralControl: MonoBehaviour {
        #region Fields

        private AudioSource[] audioSrcs;

        internal static AudioCentralControl globalObj;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal AudioCentralControl(): base() {
            audioSrcs = System.Array.Empty<AudioSource>();
        }

        static AudioCentralControl() {
            globalObj = null;
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() { //I guess
            globalObj = this;
        }

        #endregion

        internal void InitMe() {
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
            foreach(AudioSource audioSrc in audioSrcs) {
                if(audioSrc.gameObject.name == name) {
                    audioSrc.Play();
                    break;
                }
            }
        }
    }
}