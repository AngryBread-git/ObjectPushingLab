using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    [SerializeField] private Vector3 _pushForce;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(string.Format("WindArea, other.tag is: {0}", other.tag));
        if (other.CompareTag("Boulder"))
        {
            //Debug.Log(string.Format("WindArea hit boulder {0}", other.attachedRigidbody.velocity));
            Vector3 rotatedForce = transform.rotation * _pushForce;
            other.attachedRigidbody.AddForce(rotatedForce, ForceMode.VelocityChange);

            //Debug.Log(string.Format("WindArea addforce, {0}", other.attachedRigidbody.velocity));
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(_pushForce);
        Gizmos.DrawRay(transform.position, direction);
    }
}
