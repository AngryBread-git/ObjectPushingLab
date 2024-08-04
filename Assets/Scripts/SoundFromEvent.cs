using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFromEvent : MonoBehaviour
{
    [SerializeField] private AudioSource _nextStageAudioSource;
    [SerializeField] private AudioSource _addUIAudioSource;
    [SerializeField] private AudioSource _addHatAudioSource;


    private void OnEnable()
    {
        EventCoordinator<NextStageEvent>.RegisterListener(PlayNextStageSound);
        EventCoordinator<AddUIElementEvent>.RegisterListener(PlayAddUISound);
        EventCoordinator<AddHatEvent>.RegisterListener(PlayAddHatSound);
    }
    private void OnDisable()
    {
        EventCoordinator<NextStageEvent>.UnregisterListener(PlayNextStageSound);
        EventCoordinator<AddUIElementEvent>.UnregisterListener(PlayAddUISound);
        EventCoordinator<AddHatEvent>.UnregisterListener(PlayAddHatSound);
    }


    private void PlayNextStageSound(NextStageEvent ei) 
    {
        _nextStageAudioSource.Play();
    }


    private void PlayAddUISound(AddUIElementEvent ei)
    {
        _addUIAudioSource.Play();
    }

    private void PlayAddHatSound(AddHatEvent ei)
    {
        _addHatAudioSource.Play();
    }

}
