using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSound : MonoBehaviour
{
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
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
}
