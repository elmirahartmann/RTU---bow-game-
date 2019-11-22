using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {

    public float Tension;
    private bool _pressed;

    public Transform RopeTransform;

    public Vector3 RopeNearLocalPosition;
    public Vector3 RopeFarLocalPosition;

    public AnimationCurve RopeReturnAnimation;
   

    public float ReturnTime;

    public Arrow CurrentArrow;
    public float ArrowSpeed;
    private int ArrowIndex = 0;

    public Arrow[] ArrowsPool;
   

    // Use this for initialization
    void Start () {
        RopeNearLocalPosition = RopeTransform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)) {
            _pressed = true;

            ArrowIndex++;
            if (ArrowIndex >= ArrowsPool.Length)
            {
                ArrowIndex = 0;
            }
                CurrentArrow = ArrowsPool[ArrowIndex];

            CurrentArrow.SetToRope(RopeTransform);

          
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)) {
            _pressed = false; 
            StartCoroutine(RopeReturn());
            CurrentArrow.Shot(ArrowSpeed * Tension);
            Tension = 0;

           
        }
        if (_pressed) {
            if (Tension < 1f) {
                Tension += Time.deltaTime;
            }
            RopeTransform.localPosition = Vector3.Lerp(RopeNearLocalPosition, RopeFarLocalPosition, Tension);
        }
    }

    IEnumerator RopeReturn() {
        Vector3 startLocalPosition = RopeTransform.localPosition;
        for (float f = 0; f < 1f; f += Time.deltaTime / ReturnTime) {
            RopeTransform.localPosition = Vector3.LerpUnclamped(startLocalPosition, RopeNearLocalPosition, RopeReturnAnimation.Evaluate(f));
            yield return null;
        }
        RopeTransform.localPosition = RopeNearLocalPosition;
    }

}
