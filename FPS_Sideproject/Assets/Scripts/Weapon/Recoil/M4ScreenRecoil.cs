using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4ScreenRecoil : ScreenRecoil
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

    public override void FireCameraRecoil()
    {
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), 0);
    }
}
