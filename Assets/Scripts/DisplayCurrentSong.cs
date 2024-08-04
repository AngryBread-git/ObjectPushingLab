using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCurrentSong : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        SetTextToSongName();
    }



    private void SetTextToSongName() 
    {
        string songName = _audioSource.clip.name;
        _text.text = string.Format("Current Song:<br>{0}", songName);
    }
}
