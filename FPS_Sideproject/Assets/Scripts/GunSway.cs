using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSway : MonoBehaviour
{
    public CharacterController controller;
    public PlayerInput input;
    [Header("Sway")]
    public float amount = 0.02f;
    public float maxAmount = 0.06f;
    public float smooth = 6f;

    [Header("Tilt")]
    public float rotationAmount = 4f;
    public float maxRotationAmount = 5f;
    public float smoothRotation = 12f;

    [Header("Bobbing")]
    public float speedCurve;
    public Vector3 travelLimit = Vector3.one * 0.025f;
    public Vector3 bobLimit = Vector3.one * 0.01f;
    private float curveSin { get => Mathf.Sin(speedCurve); }
    private float curveCos { get => Mathf.Cos(speedCurve); }
    private Vector3 bobPosition;
    [Header("Bob Rotation")]
    public Vector3 bobAmount;
    private Vector3 bobRotation;

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
        GetBobOffest();
        GetBobRotation();
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
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + originPosition + bobPosition, Time.deltaTime * smooth);
    }
    private void UpdateTiltSway()
    {
        float tiltX = Mathf.Clamp(xMouse * rotationAmount, -maxRotationAmount, maxRotationAmount);
        float tiltY = Mathf.Clamp(yMouse * rotationAmount, -maxRotationAmount, maxRotationAmount);

        Quaternion targetRotation = Quaternion.Euler(new Vector3(rotationX ? -tiltY : 0f, rotationY ? tiltX : 0f, rotationZ ? tiltX : 0f));
        transform.localRotation = Quaternion.Lerp(transform.localRotation, originRotation * targetRotation * Quaternion.Euler(bobRotation), Time.deltaTime * smoothRotation);
    }

    private void GetBobOffest()
    {
        if(input.moveInput != Vector2.zero)
        {
            speedCurve += Time.deltaTime * (controller.isGrounded ? 10f : 1f);
        }
        else
        {
            speedCurve += Time.deltaTime * (controller.isGrounded ? 1.5f : 1f);
        }
        bobPosition.x = (curveCos * bobLimit.x * (controller.isGrounded ? 1 : 0)) - (input.moveInput.x * travelLimit.x);
        bobPosition.y = (curveSin * bobLimit.y) - (controller.velocity.y * travelLimit.y);
        bobPosition.z = -(input.moveInput.y * travelLimit.z);
    }

    private void GetBobRotation()
    {
        bobRotation.x = (input.moveInput != Vector2.zero ? bobAmount.x * (Mathf.Sin(2 * speedCurve)) : bobAmount.x * (Mathf.Sin(2 * speedCurve) /5));
        bobRotation.y = (input.moveInput != Vector2.zero ? bobAmount.y * curveCos : 0);
        bobRotation.z = (input.moveInput != Vector2.zero ? bobAmount.z * curveCos * input.moveInput.x : 0);
    }
}