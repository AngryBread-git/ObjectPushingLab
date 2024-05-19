using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] private GameObject[] _positionObjects;
    [SerializeField] private Vector3[] _positions;
    [SerializeField] private int _targetPosition;
    [SerializeField] private float _movementDuration = 5.0f;

    void Start()
    {
        SavePositionsFromObjects();

        StartCoroutine(LerpPosition(_positions[_targetPosition], _movementDuration));
    }

    private void SavePositionsFromObjects() 
    {
        for (int i = 0; i < _positionObjects.Length; i++) 
        {
            _positions[i] = _positionObjects[i].transform.position;
        }
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        TargetNewLocation();

    }

    private void TargetNewLocation() 
    {
        if (_targetPosition < _positions.Length - 1)
        {
            
            _targetPosition += 1;
            //Debug.Log(string.Format("move to next target: {0}", _targetPosition));
            StartCoroutine(LerpPosition(_positions[_targetPosition], _movementDuration));
        }

        else 
        {
            _targetPosition = 0;
            //Debug.Log(string.Format("move to first target: {0}", _targetPosition));
            StartCoroutine(LerpPosition(_positions[_targetPosition], _movementDuration));
        }

        Debug.Log(string.Format("_targetPosition: {0}", _targetPosition));
    }

}
