using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Audio
{
    public class AudioClips : Singleton<AudioClips>
    {
        [HideInInspector] public AudioClipEntry RatDeath => new(ratDeathSounds[Random.Range(0, ratDeathSounds.Count)],RatDeathReverbPreset);
        [field: SerializeField] public AudioReverbPreset RatDeathReverbPreset  { get; private set;  } = AudioReverbPreset.Off;
        [field: SerializeField] private List<AudioClip> ratDeathSounds;
        
        [HideInInspector] public AudioClipEntry CheesePickup => new(cheesePickupSounds[Random.Range(0, cheesePickupSounds.Count)],CheesePickupReverbPreset);
        [field: SerializeField] public AudioReverbPreset CheesePickupReverbPreset  { get; private set;  } = AudioReverbPreset.Off;
        [field: SerializeField] private List<AudioClip> cheesePickupSounds;
        
        [HideInInspector] public AudioClipEntry BallThrow => new(ballThrowSounds[Random.Range(0, ballThrowSounds.Count)],BallThrowReverbPreset);
        [field: SerializeField] public AudioReverbPreset BallThrowReverbPreset  { get; private set;  } = AudioReverbPreset.Off;
        [field: SerializeField] private List<AudioClip> ballThrowSounds;
        
        [HideInInspector] public AudioClipEntry ToothpickStab => new(toothpickStabSounds[Random.Range(0, toothpickStabSounds.Count)],ToothpickStabReverbPreset);
        [field: SerializeField] public AudioReverbPreset ToothpickStabReverbPreset  { get; private set;  } = AudioReverbPreset.Off;
        [field: SerializeField] private List<AudioClip> toothpickStabSounds;
        
        [HideInInspector] public AudioClipEntry PlayerJump => new(playerJumpSounds[Random.Range(0, playerJumpSounds.Count)],PlayerJumpReverbPreset);
        [field: SerializeField] public AudioReverbPreset PlayerJumpReverbPreset  { get; private set;  } = AudioReverbPreset.Off;
        [field: SerializeField] private List<AudioClip> playerJumpSounds;
        
        [HideInInspector] public AudioClipEntry GameLose => new(gameLoseSounds[Random.Range(0, gameLoseSounds.Count)],GameLoseReverbPreset);
        [field: SerializeField] public AudioReverbPreset GameLoseReverbPreset  { get; private set;  } = AudioReverbPreset.Off;
        [field: SerializeField] private List<AudioClip> gameLoseSounds;
        
        [HideInInspector] public AudioClipEntry GameWin => new(gameWinSounds[Random.Range(0, gameWinSounds.Count)],GameWinReverbPreset);
        [field: SerializeField] public AudioReverbPreset GameWinReverbPreset  { get; private set;  } = AudioReverbPreset.Off;
        [field: SerializeField] private List<AudioClip> gameWinSounds;

        public AudioClip RandomSampleAudio => sampleAudios[Random.Range(0, sampleAudios.Count)];
        private List<AudioClip> sampleAudios
        {
            get
            {
                var returnVal = new List<AudioClip>();
                returnVal.AddRange(ratDeathSounds);
                returnVal.AddRange(cheesePickupSounds);
                returnVal.AddRange(ballThrowSounds);
                returnVal.AddRange(toothpickStabSounds);
                returnVal.AddRange(playerJumpSounds);
                return returnVal;
            }
        }

        protected override void Init()
        {
            
        }
    }

    public class AudioClipEntry
    {
        public AudioClipEntry(AudioClip audioClip, AudioReverbPreset reverbPreset)
        {
            AudioClip = audioClip;
            ReverbPreset = reverbPreset;
        }
        public AudioClip AudioClip { get; private set;  }
        public AudioReverbPreset ReverbPreset  { get; private set;  } = AudioReverbPreset.Off;
    }
}