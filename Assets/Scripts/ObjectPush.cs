using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPush : MonoBehaviour
{
    [SerializeField] private float _pushForce;

    [SerializeField] private bool _isPushing;

    [SerializeField] private CharacterMovementV1 _charMovementV1;

    public bool IsPushing 
    {
        set { _isPushing = value; }
    }

    private void Awake()
    {
        _charMovementV1 = GetComponent<CharacterMovementV1>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
        {
            return;
        }
        else if (hit.moveDirection.y < -0.3f)
        {
            return;
        }

        else if (!_isPushing) 
        {
            _charMovementV1.FallOver();
            return;
        }

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        //Debug.Log(string.Format("PushDir is: {0}", pushDir));

        //body.velocity = pushDir * _pushForce;
        body.AddForce(pushDir, ForceMode.Force);
    }
}
