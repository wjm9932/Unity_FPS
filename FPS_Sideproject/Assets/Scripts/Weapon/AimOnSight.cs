using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimOnSight : MonoBehaviour
{
    public Transform weaponPosition;

    [Header("Aimmig")]
    public Transform aim;
    public float adsSpeed = 8f;

    private Vector3 originalPosition;
    private PlayerInput input;
    // Start is called before the first frame update
    void Start()
    {
        input = transform.root.gameObject.GetComponent<PlayerInput>();

        originalPosition = weaponPosition.localPosition;
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
            weaponPosition.localPosition = Vector3.Lerp(weaponPosition.localPosition, aim.localPosition, Time.deltaTime * adsSpeed);
        }
        else
        {
            weaponPosition.localPosition = Vector3.Lerp(weaponPosition.localPosition, originalPosition, Time.deltaTime * adsSpeed);
        }
    }
}
