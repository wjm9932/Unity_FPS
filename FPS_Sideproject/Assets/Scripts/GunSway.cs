using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSway : MonoBehaviour
{
    [Header("Sway")]
    public float amount = 0.02f;
    public float maxAmount = 0.06f;
    public float smooth = 6f;

    [Header("Tilt")]
    public float rotationAmount = 4f;
    public float maxRotationAmount = 5f;
    public float smoothRotation = 12f;

    [Space]
    public bool rotationX = true; 
    public bool rotationY = true;
    public bool rotationZ = true;

    private Quaternion originRotation;
    private Vector3 originPosition;
    private float xMouse;
    private float yMouse;
    // Start is called before the first frame update
    void Start()
    {
        originRotation = transform.localRotation;
        originPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseValue();
        UpdateSway();
        UpdateTiltSway();
    }

    private void GetMouseValue()
    {
        xMouse = -Input.GetAxis("Mouse X");
        yMouse = -Input.GetAxis("Mouse Y");
    }
    private void UpdateSway()
    {
        float moveX = Mathf.Clamp(xMouse * amount, -maxAmount, maxAmount);
        float moveY = Mathf.Clamp(yMouse * amount, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(moveX, moveY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + originPosition, Time.deltaTime * smooth);
    }
    private void UpdateTiltSway()
    {
        float tiltX = Mathf.Clamp(xMouse * rotationAmount, -maxRotationAmount, maxRotationAmount);
        float tiltY = Mathf.Clamp(yMouse * rotationAmount, -maxRotationAmount, maxRotationAmount);

        Quaternion targetRotation = Quaternion.Euler(new Vector3(rotationX ? -tiltY: 0f, rotationY? tiltX : 0f, rotationZ? tiltX : 0f));
        transform.localRotation = Quaternion.Lerp(transform.localRotation, originRotation * targetRotation, Time.deltaTime * smoothRotation);
    }
}
