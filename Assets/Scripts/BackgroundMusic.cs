using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClips;
    private float _orgVolume;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _orgVolume = _audioSource.volume;
    }

    private void OnEnable()
    {
        EventCoordinator<ChangeMusicEventInfo>.RegisterListener(ChangeBackgroundMusic);
    }
    private void OnDisable()
    {
        EventCoordinator<ChangeMusicEventInfo>.UnregisterListener(ChangeBackgroundMusic);
    }


    private void ChangeBackgroundMusic(ChangeMusicEventInfo ei) 
    {
        //fade-out, then change song, then fade-in.
    }
}
