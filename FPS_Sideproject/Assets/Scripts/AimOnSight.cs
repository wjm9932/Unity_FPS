using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimOnSight : MonoBehaviour
{
    [Header("Aimmig")]
    public Vector3 aimPosition;
    public float adsSpeed = 8f;

    private Vector3 originalPosition;
    private PlayerInput input;
    // Start is called before the first frame update
    void Start()
    {
        input = transform.root.gameObject.GetComponent<PlayerInput>();

        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        AimDownSight();
    }

    private void AimDownSight()
    {
        if(input.isAiming == true)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * adsSpeed);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * adsSpeed);
        }
    }
}
