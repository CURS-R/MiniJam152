using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Audio
{
    public class AudioManager_Temp : Singleton<AudioManager_Temp>
    {
        [field: SerializeField] public AudioObject audioObjectPrefab;
        public GameObject AudioObjectPrefab => audioObjectPrefab.gameObject;


        private readonly List<AudioObject> audioObjectGOs = new();
        private List<AudioObject> audioObjects
        {
            get
            {
                audioObjectGOs.RemoveAll(item => item == null);
                return audioObjects.Select(go => go.GetComponent<AudioObject>()).ToList();
            }
        }
        
        public void PlayASound(AudioClip audio, Vector3 pos)
        {
            var newAudioObjectGO = Instantiate(audioObjectPrefab, pos, Quaternion.identity);
            audioObjectGOs.Add(newAudioObjectGO);
            newAudioObjectGO.transform.position = pos;
            var newAudioObject = newAudioObjectGO.GetComponent<AudioObject>();
            newAudioObject.AudioSource.clip = audio;
            newAudioObject.AudioSource.Play();
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

        #region AMBIENT STUFF
        
        /*
        private static bool onCooldown;

        [SerializeField] private AmbientAudioConfigurationSO ambientAudioConfigurationSO;
        [SerializeField] private AudioSource AmbientAudioPlayer;
        [SerializeField] private Vector2 AmbientAudioCooldown;

        protected override void Init()
        {
            onCooldown = false;
        }

        private void Update()
        {
            if (!onCooldown)
            {
                // TODO: ambient audio?
                //StartCoroutine(AmbientSoundTimer());
            }
        }

        private IEnumerator AmbientSoundTimer()
        {
            onCooldown = true;
            yield return new WaitForSeconds(Random.Range(AmbientAudioCooldown.x, AmbientAudioCooldown.y));
            PlayAnAmbientSound(false);
            onCooldown = false;
            yield return new WaitForSeconds(AmbientAudioCooldown.x);
        }

        public void PlayAnAmbientSound(bool usePlayersLocation)
        {
            AmbientAudioPlayer.clip = ambientAudioConfigurationSO.GetRandomAmbientAudio();
            AmbientAudioPlayer.transform.position = usePlayersLocation ? Vector3.zero : GetRandomAmbientLocation();
            AmbientAudioPlayer.Play();
        }

        private Vector3 GetRandomAmbientLocation()
        {
            if (ambientAudioConfigurationSO.ambientAudioLocations == null ||
                ambientAudioConfigurationSO.ambientAudioLocations.Count < 1)
            {
                return this.transform.position;
            }

            return ambientAudioConfigurationSO.ambientAudioLocations[
                Random.Range(0, ambientAudioConfigurationSO.ambientAudioLocations.Count)].position;
        }
        */
        
        #endregion
    }
}
