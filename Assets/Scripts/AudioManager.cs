using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   [Header("Music")]
   [SerializeField] private  AudioSource _backGroundMusic;

    [Header("Weapons")]
    [SerializeField] private AudioSource _ballSound;
    [SerializeField] private AudioSource _toothPickImpact;

    public void BallSound()
    {
        _ballSound.Play();
    }
}
