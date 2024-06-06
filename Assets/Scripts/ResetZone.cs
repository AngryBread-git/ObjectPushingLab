using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetZone : MonoBehaviour
{
    [SerializeField] private GameObject _boulderStartObject;
    [SerializeField] private GameObject _boulder;
    private Vector3 _boulderStartTransform;

    [SerializeField] GameObject _playerStartObject;
    [SerializeField] private GameObject _player;
    private Vector3 _playerStartTransform;

    [SerializeField] private DialogueTriggerBox _dialogueTriggerBox;


    private void Start()
    {
        _boulderStartTransform = _boulderStartObject.transform.position;
        _playerStartTransform = _playerStartObject.transform.position;

    }

    private void OnEnable()
    {
        EventCoordinator<NextStageEvent>.RegisterListener(ResetObjects);
    }
    private void OnDisable()
    {
        EventCoordinator<NextStageEvent>.UnregisterListener(ResetObjects);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(string.Format("ResetZone, other.tag is: {0}", other.tag));
        if (other.CompareTag("Boulder"))
        {
            Debug.Log(string.Format("Boulder pos is: {0}", _boulder.transform.position));
            _boulder.transform.position = _boulderStartTransform;
        }
        else if (other.CompareTag("Player"))
        {
            //Debug.Log(string.Format("other name is: {0}", other.gameObject));
            Debug.Log(string.Format("Player pos is: {0}", other.gameObject.transform.position));
            _player.GetComponent<CharacterController>().enabled = false;
            _player.transform.position = _playerStartTransform;
            _player.GetComponent<CharacterController>().enabled = true;
            Debug.Log(string.Format("Player pos is now: {0}", other.gameObject.transform.position));

            _dialogueTriggerBox.StartSideDialogue();
        }
    }

    private void ResetObjects(NextStageEvent ei)
    {
        Debug.Log(string.Format("ResetZone: called by ResetObjectsEvent"));


        _boulder.transform.position = _boulderStartTransform;

        _player.GetComponent<CharacterController>().enabled = false;
        _player.transform.position = _playerStartTransform;
        _player.GetComponent<CharacterController>().enabled = true;
    }
}
