using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetZone : MonoBehaviour
{
    [SerializeField] private GameObject _boulderStartObject;
    private Vector3 _boulderStartTransform;

    [SerializeField] GameObject _playerStartObject;
    private Vector3 _playerStartTransform;
    private void Start()
    {
        _boulderStartTransform = _boulderStartObject.transform.position;
        _playerStartTransform = _playerStartObject.transform.position;
    }

    private void OnEnable()
    {
        EventCoordinator<ResetObjectsEvent>.RegisterListener(ResetObjects);
    }
    private void OnDisable()
    {
        EventCoordinator<ResetObjectsEvent>.UnregisterListener(ResetObjects);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(string.Format("ResetZone, other.tag is: {0}", other.tag));
        if (other.CompareTag("Boulder"))
        {
            Debug.Log(string.Format("Boulder pos is: {0}", other.gameObject.transform.position));
            other.transform.position = _boulderStartTransform;
        }
        else if (other.CompareTag("Player"))
        {
            //Debug.Log(string.Format("other name is: {0}", other.gameObject));
            Debug.Log(string.Format("Player pos is: {0}", other.gameObject.transform.position));
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.gameObject.transform.position = _playerStartTransform;
            other.gameObject.GetComponent<CharacterController>().enabled = true;
            Debug.Log(string.Format("Player pos is now: {0}", other.gameObject.transform.position));
        }
    }

    private void ResetObjects(ResetObjectsEvent ei)
    {
        Debug.Log(string.Format("ResetZone: called by ResetObjectsEvent"));

        //_autoPrintNextLine = ei._isAutoNextLine;
        //other.transform.position = _boulderStartTransform;


        //other.gameObject.GetComponent<CharacterController>().enabled = false;
        //other.gameObject.transform.position = _playerStartTransform;
        //other.gameObject.GetComponent<CharacterController>().enabled = true;
    }
}
