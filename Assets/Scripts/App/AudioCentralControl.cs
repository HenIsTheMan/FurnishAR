using UnityEngine;

namespace FurnishAR.App {
    internal sealed class AudioCentralControl: MonoBehaviour {
        #region Fields

        private AudioSource[] audioSrcs;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal AudioCentralControl(): base() {
            audioSrcs = System.Array.Empty<AudioSource>();
        }

        static AudioCentralControl() {
        }

        #endregion

        #region Unity User Callback Event Funcs
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