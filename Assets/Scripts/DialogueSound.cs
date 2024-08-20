using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSound : MonoBehaviour
{
    [SerializeField]private AudioSource _audioSource;
    private float _soundDelay;
    private bool _allowSound = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void PlaySoundFromOptions() 
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.pitch = Random.Range(0.85f, 1.15f);
            _audioSource.Play();
        }
    }

    private void OnEnable()
    {
        EventCoordinator<PlayDialogueSoundEvent>.RegisterListener(PlayDialogueSound);
    }
    private void OnDisable()
    {
        EventCoordinator<PlayDialogueSoundEvent>.UnregisterListener(PlayDialogueSound);
    }

    private void PlayDialogueSound(PlayDialogueSoundEvent ei) 
    {
        //Debug.Log(string.Format("PlayDialogueSound, _allowSound is: {0}", _allowSound));
        if (_allowSound) 
        {
            _audioSource.pitch = Random.Range(0.95f, 1.05f);
            _audioSource.Play();
            _allowSound = false;

            float delay = Random.Range(0.1f, 0.2f);
            StartCoroutine(AllowSoundAfterDelay(delay));
        }
    }

    private IEnumerator AllowSoundAfterDelay(float delay) 
    {
        //Debug.Log(string.Format("before delay, _allowSound is: {0}", _allowSound));
        yield return new WaitForSeconds(delay);
        _allowSound = true;
        //Debug.Log(string.Format("after delay, _allowSound is: {0}", _allowSound));
    }

    //To prevent a sound being played when the audio settings are loaded.
    private IEnumerator FadeIn() 
    {
        float orgVolume = _audioSource.volume;
        _audioSource.volume = 0.0f;

        yield return new WaitForSeconds(0.1f);
        _audioSource.volume = orgVolume;
    }
}
