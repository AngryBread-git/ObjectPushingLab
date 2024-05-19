using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    [SerializeField] Vector3 _pushForce;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(string.Format("WindArea, other.tag is: {0}", other.tag));
        if (other.CompareTag("Boulder"))
        {
            Debug.Log(string.Format("WindArea hit boulder {0}", other.attachedRigidbody.velocity));
            other.attachedRigidbody.AddForce(_pushForce, ForceMode.VelocityChange);

            Debug.Log(string.Format("WindArea addforce, {0}", other.attachedRigidbody.velocity));
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(_pushForce);
        Gizmos.DrawRay(transform.position, direction);
    }
}
