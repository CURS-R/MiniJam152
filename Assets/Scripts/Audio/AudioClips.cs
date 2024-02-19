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
        [HideInInspector] public AudioClip RatDeath => ratDeathSounds[Random.Range(0, ratDeathSounds.Count)];
        [field: SerializeField] private List<AudioClip> ratDeathSounds;
        
        [HideInInspector] public AudioClip CheesePickup => cheesePickupSounds[Random.Range(0, cheesePickupSounds.Count)];
        [field: SerializeField] private List<AudioClip> cheesePickupSounds;
        
        [HideInInspector] public AudioClip BallThrow => ballThrowSounds[Random.Range(0, ballThrowSounds.Count)];
        [field: SerializeField] private List<AudioClip> ballThrowSounds;
        
        [HideInInspector] public AudioClip ToothpickStab => toothpickStabSounds[Random.Range(0, toothpickStabSounds.Count)];
        [field: SerializeField] private List<AudioClip> toothpickStabSounds;
        
        [HideInInspector] public AudioClip PlayerJump => playerJumpSounds[Random.Range(0, playerJumpSounds.Count)];
        [field: SerializeField] private List<AudioClip> playerJumpSounds;
        
        [HideInInspector] public AudioClip GameLose => gameLoseSounds[Random.Range(0, gameLoseSounds.Count)];
        [field: SerializeField] private List<AudioClip> gameLoseSounds;
        
        [HideInInspector] public AudioClip GameWin => gameWinSounds[Random.Range(0, gameWinSounds.Count)];
        [field: SerializeField] private List<AudioClip> gameWinSounds;
        
        protected override void Init()
        {
            
        }
    }
}