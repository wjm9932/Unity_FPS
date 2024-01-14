using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClipPrevention : MonoBehaviour
{
    public GameObject clipProjector;
    public float checkDistance;
    public Vector3 newDirection;

    private bool isClipping;
    private float lerpPos;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        isClipping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isClipping == false)
        {
            lerpPos = Mathf.Lerp(lerpPos, 0f, 10f*Time.deltaTime);
        }

        if(Physics.Raycast(clipProjector.transform.position, clipProjector.transform.forward, out hit, checkDistance) == true)
        {
            isClipping = true;
            lerpPos = Mathf.Lerp(lerpPos, 1 - (hit.distance / checkDistance), 10f * Time.deltaTime);
        }
        else
        {
            isClipping = false;
        }

        Mathf.Clamp01(lerpPos);
        transform.localRotation = Quaternion.Lerp(Quaternion.Euler(Vector3.zero), Quaternion.Euler(newDirection), lerpPos);
    }
}
