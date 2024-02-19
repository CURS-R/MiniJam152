using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // Externals
    public PlayerConfiguration playerConfigurationSO;
    public GameObject pauseUIGO;
    public Slider volumeSlider, sensitivitySlider;
    
    // Internals
    private AudioListener audioListener;
    private bool toggled;
    private bool initialized = false;

    private void Awake()
    {
        AudioListener.volume = PlayerConfiguration.Volume;
        sensitivitySlider.minValue = PlayerConfiguration.SENSITIVITY_MIN;
        sensitivitySlider.maxValue = PlayerConfiguration.SENSITIVITY_MAX;
        sensitivitySlider.value = PlayerConfiguration.CameraSensitivity;
        volumeSlider.minValue = 0;
        volumeSlider.value = PlayerConfiguration.Volume;
        volumeSlider.maxValue = 1;
        initialized = true;
        toggled = false;
        TogglePauseMenu();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
            TogglePauseMenu();
        if (toggled)
        {
            SetAudioVolume();
            SetSensitivity();
        }
    }

    private void TogglePauseMenu()
    {
        Time.timeScale = toggled ? 0 : 1;
        Cursor.lockState = toggled ? CursorLockMode.None : CursorLockMode.Locked;
        pauseUIGO.SetActive(toggled);
        toggled = !toggled;
    }
    
    public void SetAudioVolume()
    {
        if (!initialized)
            return;
        
        //mixer.SetFloat("MusicVol", Mathf.Log10(volumeSlider.value) * 20);
        PlayerConfiguration.Volume = volumeSlider.value;
        AudioListener.volume = PlayerConfiguration.Volume;
    }

    public void SetSensitivity()
    {
        if (!initialized)
            return;
        
        PlayerConfiguration.CameraSensitivity = sensitivitySlider.value;
    }
    
    public void TestAudio()
    {
        FindObjectOfType<AudioManager>().PlayASound();
    }
}
