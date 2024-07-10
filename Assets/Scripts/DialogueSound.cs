using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSound : MonoBehaviour
{
    [SerializeField]private AudioSource _audioSource;

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
        if (!_audioSource.isPlaying) 
        {
            _audioSource.pitch = Random.Range(0.85f, 1.15f);
            _audioSource.Play();
        }

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
