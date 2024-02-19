using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Utils;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("Music")]
        [SerializeField] private  AudioSource _backGroundMusic;

        [field: SerializeField] private PlayerConfiguration playerConfiguration;
        [field: SerializeField] private AudioMixerGroup audioMixerGroup;
        [field: SerializeField] private AudioObject audioObjectPrefab;
        public GameObject AudioObjectPrefab => audioObjectPrefab.gameObject;

        private readonly List<AudioObject> audioObjectGOs = new();
        private List<AudioObject> audioObjects
        {
            get
            {
                audioObjectGOs.RemoveAll(item => item == null);
                return audioObjectGOs.Select(go => go.GetComponent<AudioObject>()).ToList();
            }
        }
        
        public void PlayASound()
        {
            var pos = FindObjectOfType<PlayerScript>().transform.position; // HACK:
            AudioClipEntry randomClipEntry = new(AudioClips.Instance.RandomSampleAudio, AudioReverbPreset.Off);
            PlayASound(randomClipEntry, pos);
        }
        public void PlayASound(AudioClipEntry audioClipEntry, Vector3 pos)
        {
            var newAudioObjectGO = Instantiate(audioObjectPrefab, pos, Quaternion.identity);
            audioObjectGOs.Add(newAudioObjectGO);
            newAudioObjectGO.transform.position = pos;
            var newAudioObject = newAudioObjectGO.GetComponent<AudioObject>();
            newAudioObject.AudioReverbFilter.reverbPreset = audioClipEntry.ReverbPreset;
            var audioSource = newAudioObject.AudioSource;
            //audioSource.volume = PlayerConfiguration.Volume;
            audioSource.outputAudioMixerGroup = audioMixerGroup;
            audioSource.clip = audioClipEntry.AudioClip;
            audioSource.Play();
        }

        protected override void Init()
        {
        }

        private void Update()
        {
            foreach (var audioObject in audioObjects)
            {
                if (!audioObject.AudioSource.isPlaying)
                    Destroy(audioObject.gameObject);
            }
        }
    }
}
