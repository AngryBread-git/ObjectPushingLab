using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatAdder : MonoBehaviour
{
    [SerializeField] private GameObject[] _hats;
    private int hatNr = 0;

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

        if (hatNr < _hats.Length) 
        {
            _hats[hatNr].SetActive(true);
        }

        hatNr += 1;

        if (hatNr == _hats.Length)
        {
            Debug.Log(string.Format("Final HatAdd. make them taller."));
            Vector3 scale = new Vector3(0, 1.5f, 0);
            foreach (GameObject hat in _hats)
            {
                //Debug.Log(string.Format("Final HatAdd. make them taller."));

                hat.transform.localScale = scale;
            }
        }

        Debug.Log(string.Format("hatNr:{0}", hatNr));
    }
}
