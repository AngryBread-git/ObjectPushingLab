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

    public void AddHat(int hatNr) 
    {
        _hats[hatNr].SetActive(true);

    }
}
