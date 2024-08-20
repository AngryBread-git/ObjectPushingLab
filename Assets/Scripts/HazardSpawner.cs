using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _hazardList1;
    [SerializeField] private List<GameObject> _hazardList2;
    [SerializeField] private List<GameObject> _hazardList3;
    [SerializeField] private List<GameObject> _hazardList4;
    private int _nextStageNr = 0;

    private void OnEnable()
    {
        EventCoordinator<NextStageEvent>.RegisterListener(SpawnHazard);
    }
    private void OnDisable()
    {
        EventCoordinator<NextStageEvent>.UnregisterListener(SpawnHazard);
    }

    private void SpawnHazard(NextStageEvent ei) 
    {
        _nextStageNr += 1;
        //Debug.Log(string.Format("HazardSpawner: _nextStageNr is {0}", _nextStageNr));
        List<GameObject> currentList = SelectHazardList(_nextStageNr);

        if (currentList == null) 
        {
            //Debug.LogWarning(string.Format("HazardSpawner: no list was returned"));
            return;
        }
        else if (currentList.Count == 0) 
        {
            //Debug.LogWarning(string.Format("HazardSpawner: currentList is empty"));
            return;
        }

        foreach (GameObject gameObject in currentList) 
        {
            gameObject.SetActive(true);
        }
    }

    private List<GameObject> SelectHazardList(int i)
    {
        switch (i)
        {
            case 1:
                return _hazardList1;
            case 2:
                return _hazardList2;
            case 4:
                return _hazardList3;
            case 7:
                return _hazardList4;
            //etc
            default:
                //Debug.LogWarning(string.Format("HazardSpawner: {0} not found", i));
                return null;
        }
    }
}
