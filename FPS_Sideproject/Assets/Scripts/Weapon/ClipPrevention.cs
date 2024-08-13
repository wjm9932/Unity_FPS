using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClipPrevention : MonoBehaviour
{
    public GameObject clipProjector;
    public float checkDistance;

    private Vector3 newDirection;
    private float lerpPos;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        newDirection = new Vector3(0f, -90f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(clipProjector.transform.position, clipProjector.transform.forward, out hit, checkDistance) == true)
        {
            lerpPos = Mathf.Lerp(lerpPos, 1 - (hit.distance / checkDistance), 10f * Time.deltaTime);
        }
        else
        {
            lerpPos = Mathf.Lerp(lerpPos, 0f, 10f * Time.deltaTime);

        }

        Mathf.Clamp01(lerpPos);
        transform.localRotation = Quaternion.Slerp(Quaternion.Euler(Vector3.zero), Quaternion.Euler(newDirection), lerpPos);
    }
}
