using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiUpdater : MonoBehaviour
{
    [SerializeField] private GameObject[] _uiObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        EventCoordinator<AddUIElementEvent>.RegisterListener(AddUIElement);
        EventCoordinator<AddUIElementEvent>.RegisterListener(RemoveUIElement);
    }
    private void OnDisable()
    {
        EventCoordinator<AddUIElementEvent>.UnregisterListener(AddUIElement);
        EventCoordinator<AddUIElementEvent>.UnregisterListener(RemoveUIElement);
    }


    private void AddUIElement(AddUIElementEvent ei)
    {
        if (ei._UIElementNr < _uiObjects.Length)
        {
            _uiObjects[ei._UIElementNr].SetActive(true);


        }
    }

    private void RemoveUIElement(AddUIElementEvent ei)
    {
        //Disable the DiamondBadge when the EmeraldBadge is shown. 
        //Yes, hard-coded values are bad. I know.
        if (ei._UIElementNr == 1)
        {
            _uiObjects[0].SetActive(false);
        }


        //Disable the draft-cards when the Exercise card is picked.
        if (ei._UIElementNr == 3)
        {
            _uiObjects[2].SetActive(false);
        }
    }

}
