using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatAdder : MonoBehaviour
{
    [SerializeField] private GameObject[] _hats;

    private void Start()
    {  
        //AddHat(0); 
    }

    private void OnEnable()
    {
        EventCoordinator<AddHatEvent>.RegisterListener(AddHat);
    }
    private void OnDisable()
    {
        EventCoordinator<AddHatEvent>.UnregisterListener(AddHat);
    }

    private void AddHat(AddHatEvent ei) 
    {
        if (ei._hatNr < _hats.Length) 
        {
            _hats[ei._hatNr].SetActive(true);
        }

    }
}
