using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _musicMixer;
    [SerializeField] private Slider _musicSlider;

    [SerializeField] private AudioMixerGroup _effectsMixer;
    [SerializeField] private Slider _effectsSlider;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(string.Format("AudioSettings Start."));
        LoadSavedSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadSavedSettings()
    {
        float tempMusicVolume = PlayerPrefs.GetFloat("musicVolume", 0.9f);
        _musicMixer.audioMixer.SetFloat("BGM_Volume", LinearToDecibel(tempMusicVolume));
        _musicSlider.value = tempMusicVolume;
        //Debug.Log(string.Format("_musicSlider is {0}.", _musicSlider.value));


        float tempEffectsVolume = PlayerPrefs.GetFloat("effectsVolume", 0.9f);
        _effectsMixer.audioMixer.SetFloat("SFX_Volume", LinearToDecibel(tempEffectsVolume));
        _effectsSlider.value = tempEffectsVolume;
    }

    public void UpdateMusicVolume(float sliderValue)
    {
        float dBValue = LinearToDecibel(sliderValue);

        _musicMixer.audioMixer.SetFloat("BGM_Volume", dBValue);
        PlayerPrefs.SetFloat("musicVolume", sliderValue);

    }

    public void UpdateEffectsVolume(float sliderValue)
    {

        float dBValue = LinearToDecibel(sliderValue);

        _effectsMixer.audioMixer.SetFloat("SFX_Volume", dBValue);
        PlayerPrefs.SetFloat("effectsVolume", sliderValue);

    }


    private float LinearToDecibel(float linear)
    {
        float dB;
        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f;
        return dB;
    }

    private float DecibelToLinear(float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20.0f);
        return linear;
    }
}
