using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClips;
    private int _currentAudioClip = 0;
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

        if (_audioClips.Length <= _currentAudioClip)
        {
            _currentAudioClip = 0;
            StartCoroutine(CrossFace(_currentAudioClip));
        }
        else 
        {
            _currentAudioClip += 1;
            StartCoroutine(CrossFace(_currentAudioClip));
        }
        
    }

    private IEnumerator CrossFace(int audioClipNr) 
    {
        yield return StartCoroutine(FadeVolume(2.0f,0));
        _audioSource.clip = _audioClips[audioClipNr];
        _audioSource.Play();
        //Potential problem. It seems that the audio fades back in after going back to the first song.
        yield return StartCoroutine(FadeVolume(2.0f, _orgVolume));
    }

    private IEnumerator FadeVolume(float duration, float targetVolume)
    {
        float currentTime = 0;

        float startVolume = _audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
