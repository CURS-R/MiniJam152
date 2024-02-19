using UnityEngine;

namespace Audio
{
    public class AudioObject : MonoBehaviour
    {
        [field: SerializeField] public AudioSource AudioSource;
        [field: SerializeField] public AudioReverbFilter AudioReverbFilter;
    }
}