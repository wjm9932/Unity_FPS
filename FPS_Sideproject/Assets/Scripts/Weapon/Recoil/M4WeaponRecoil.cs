using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4WeaponRecoil : WeaponRecoil
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void FireWeaponRecoil()
    {
        positionalRecoil += new Vector3(Random.Range(-recoilKickBack.x, recoilKickBack.x), Random.Range(-recoilKickBack.y, recoilKickBack.y), recoilKickBack.z);
        rotationalRecoil += new Vector3(-recoilRotation.x, Random.Range(-recoilRotation.y, recoilRotation.y), Random.Range(-recoilRotation.z, recoilRotation.z));
    } 
}
