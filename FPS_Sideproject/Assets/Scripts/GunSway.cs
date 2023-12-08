using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSway : MonoBehaviour
{
    public float intensity;
    public float smooth;

    private Quaternion originRotation;
    // Start is called before the first frame update
    void Start()
    {
        originRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSway();
    }

    private void UpdateSway()
    {
        float txMouse = Input.GetAxis("Mouse X");
        float tyMouse = Input.GetAxis("Mouse Y");

        //calculate target rotation
        Quaternion target_X_Adjustment = Quaternion.AngleAxis(-intensity * txMouse, Vector3.up);
        Quaternion target_Y_Adjustment = Quaternion.AngleAxis(intensity * tyMouse, Vector3.right);
        Quaternion targetRotation = originRotation * target_X_Adjustment * target_Y_Adjustment;

        //rotate toward target rotation;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
    }
}
