using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerConfiguration : ScriptableObject
{
    // Consts
    private const string SENSITIVITY_KEY = "PlayerConfigurationSensitivity";
    private const string VOLUME_KEY = "PlayerConfigurationVolume";
    public const float SENSITIVITY_MIN = 0.001f;
    public const float SENSITIVITY_MAX = 20f;
    
    [HideInInspector]
    public bool gameEnd;

    // Properties
    public static float CameraSensitivity {
        get => Mathf.Clamp(PlayerPrefs.GetFloat(SENSITIVITY_KEY, 2f), SENSITIVITY_MIN, SENSITIVITY_MAX);
        set => PlayerPrefs.SetFloat(SENSITIVITY_KEY, Mathf.Clamp(value, SENSITIVITY_MIN, SENSITIVITY_MAX));
    }
    public static float Volume {
        get => Mathf.Clamp(PlayerPrefs.GetFloat(VOLUME_KEY, 1), 0, 1);
        set => PlayerPrefs.SetFloat(VOLUME_KEY, Mathf.Clamp(value, 0, 1));
    }
}
