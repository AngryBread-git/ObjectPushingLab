using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetZone : MonoBehaviour
{
    [SerializeField] private GameObject _boulderStartObject;
    [SerializeField] private GameObject _boulderTopObject;
    [SerializeField] private GameObject _boulder;
    private Vector3 _boulderStartTransform;
    private Vector3 _boulderTopTransform;

    [SerializeField] GameObject _playerStartObject;
    [SerializeField] GameObject _playerTopObject;
    [SerializeField] private GameObject _player;
    private Vector3 _playerStartTransform;
    private Vector3 _playerTopTransform;

    private void Start()
    {
        _boulderStartTransform = _boulderStartObject.transform.position;
        _playerStartTransform = _playerStartObject.transform.position;

        _boulderTopTransform = _boulderTopObject.transform.position;
        _playerTopTransform = _playerTopObject.transform.position;

    }

    private void OnEnable()
    {
        EventCoordinator<NextStageEvent>.RegisterListener(ResetObjects);
        EventCoordinator<ResetPositionEvent>.RegisterListener(ResetObjectsFromPlayer);
        EventCoordinator<TeleportToTopEvent>.RegisterListener(TeleportToTop);
    }
    private void OnDisable()
    {
        EventCoordinator<NextStageEvent>.UnregisterListener(ResetObjects);
        EventCoordinator<ResetPositionEvent>.UnregisterListener(ResetObjectsFromPlayer);
        EventCoordinator<TeleportToTopEvent>.UnregisterListener(TeleportToTop);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(string.Format("ResetZone, other.tag is: {0}", other.tag));
        if (other.CompareTag("Boulder"))
        {
            Debug.Log(string.Format("Boulder pos is: {0}", _boulder.transform.position));
            ResetBoulder();
            ResetPlayer();
        }
        else if (other.CompareTag("Player"))
        {
            //Debug.Log(string.Format("other name is: {0}", other.gameObject));
            Debug.Log(string.Format("Player pos is: {0}", other.gameObject.transform.position));
            ResetBoulder();
            ResetPlayer();
            Debug.Log(string.Format("Player pos is now: {0}", other.gameObject.transform.position));

            //_dialogueTriggerBox.StartSideDialogue();
        }
    }

    private void ResetObjects(NextStageEvent ei)
    {
        Debug.Log(string.Format("ResetZone: called by NextStageEvent"));

        ResetPlayer();
        ResetBoulder();
    }

    private void ResetObjectsFromPlayer(ResetPositionEvent ei) 
    {
        Debug.Log(string.Format("ResetZone: called by ResetPositionEvent"));

        ResetPlayer();
        ResetBoulder();
    }

    private void ResetPlayer() 
    {
        _player.GetComponent<CharacterController>().enabled = false;
        _player.transform.position = _playerStartTransform;
        _player.GetComponent<CharacterController>().enabled = true;
    }

    private void ResetBoulder() 
    {
        _boulder.transform.position = _boulderStartTransform;

    }

    private void TeleportToTop(TeleportToTopEvent ei) 
    {
        _boulder.transform.position = _boulderTopTransform;

        _player.GetComponent<CharacterController>().enabled = false;
        _player.transform.position = _playerTopTransform;
        _player.GetComponent<CharacterController>().enabled = true;
    }
}
