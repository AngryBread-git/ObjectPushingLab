using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
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
        if (_audioClips.Length <= ei._audioClipNr)
        {
            Debug.LogWarning(string.Format("audioClipNr greater than amount of audioclips for BackgroundMusic."));
            StartCoroutine(CrossFace(_audioClips.Length -1));
        }
        else 
        {
            StartCoroutine(CrossFace(ei._audioClipNr));
        }
        
    }

    private IEnumerator CrossFace(int audioClipNr) 
    {
        yield return StartCoroutine(FadeVolume(2.0f,0));
        _audioSource.clip = _audioClips[audioClipNr];
        _audioSource.Play();
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
