using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private Vector3 _offset = new Vector3(0, 0, 0);
    [SerializeField] private Quaternion _quaternion = new Quaternion(0, 0, 0, 0);

    [SerializeField] private Camera _camera;
    //[SerializeField] private Transform _boulder;
    private void Start()
    {
        transform.rotation = _quaternion;
    }
    void LateUpdate()
    {
        Vector3 target = new Vector3(_player.position.x, _player.position.y, _player.position.z) + _offset;
        transform.position = target;

        //update rotation.
    }



}
