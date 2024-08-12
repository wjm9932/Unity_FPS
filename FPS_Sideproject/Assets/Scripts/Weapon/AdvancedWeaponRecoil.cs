using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedWeaponRecoil : MonoBehaviour
{
    [Header("Reference Points")]
    public Transform recoilTransform;

    [Header("Speed")]
    public float positionalRecoilSpeed = 8f;
    public float rotationalRecoilSpeed = 8f;
    
    [Space()]
    public float positionalReturnSpeed = 18f;
    public float rotationalReturnSpeed = 38f;

    [Header("Amount Setting")]
    public Vector3 recoilRotation = new Vector3(10f, 5f, 7f);
    public Vector3 recoilKickBack = new Vector3(0.015f, 0f, -0.2f);
    //[Space()]
    //public Vector3 recoilRotationAim = new Vector3(10f, 5f, 7f);
    //public Vector3 recoilKickBackAim = new Vector3(0.015f, 0f, -0.2f);

    [Space()]
    Vector3 rotationalRecoil;
    Vector3 positionalRecoil;
    Vector3 rotation;
    Vector3 originPosition;
    Vector3 originRotation;

    [Header("State")]
    public bool aiming;

    // Start is called before the first frame update
    void Start()
    {
        originPosition = recoilTransform.localPosition;
        originRotation = recoilTransform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        positionalRecoil = Vector3.Lerp(positionalRecoil, originPosition, positionalReturnSpeed * Time.deltaTime);
        rotationalRecoil = Vector3.Slerp(rotationalRecoil, originRotation, rotationalReturnSpeed * Time.deltaTime);

        recoilTransform.localPosition = Vector3.Lerp(recoilTransform.localPosition, positionalRecoil, positionalRecoilSpeed * Time.fixedDeltaTime);
        rotation = Vector3.Slerp(rotation, rotationalRecoil, rotationalRecoilSpeed* Time.fixedDeltaTime);
        recoilTransform.localRotation = Quaternion.Euler(rotation);
    }

    public void FireWeaponRecoil()
    {
        positionalRecoil += new Vector3(Random.Range(-recoilKickBack.x, recoilKickBack.x), Random.Range(-recoilKickBack.y, recoilKickBack.y), recoilKickBack.z);
        rotationalRecoil += new Vector3(-recoilRotation.x, Random.Range(-recoilRotation.y, recoilRotation.y), Random.Range(-recoilRotation.z, recoilRotation.z));
    }
}
