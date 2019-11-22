using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public Rigidbody Rigidbody;

    public TrailRenderer TrailRenderer;

    public void SetToRope(Transform ropeTransform) {
        transform.parent = ropeTransform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Rigidbody.isKinematic = true;
        TrailRenderer.enabled = false;
       



    }

    public void Shot(float velocity) {
        transform.parent = null;
        Rigidbody.isKinematic = false;
        Rigidbody.velocity = transform.forward * velocity;
        TrailRenderer.Clear();
        TrailRenderer.enabled = true;
    }


}
