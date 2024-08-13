using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSway : MonoBehaviour
{
    public CharacterController controller;
    private PlayerInput input;

    [Header("Sway")]
    public float idleSwayAmount = 0.02f;
    public float idleSwayMaxAmount = 0.06f;

    public float aimSwayAmount = 0.02f;
    public float aimSwayMaxAmount = 0.06f;

    private float swayAmount;
    private float swayMaxAmount;
    
    public float smooth = 6f;

    [Header("Tilt")]
    public float idleTiltAmount = 4f;
    public float idleMaxTiltAmount = 5f;

    public float aimTiltAmount = 4f;
    public float aimMaxTiltAmount = 5f;

    private float tiltAmount;

    public float smoothRotation = 12f;

    public bool rotationX = true;
    public bool rotationY = true;
    public bool rotationZ = true;

    [Header("Bob Position")]
    public float speedCurve;
    public float bobbingSpeedForWalking = 10f;
    public float bobbingSpeedForStanding = 1.5f;

    [Space()]
    public Vector3 idleTravelLimit;
    public Vector3 idleBobLimit;

    public Vector3 aimTravelLimit;
    public Vector3 aimBobLimit;

    private Vector3 travelLimit;
    private Vector3 bobLimit;


    [Header("Bob Rotation")]
    public Vector3 idleBobAmount;
    public Vector3 aimBobAmount;
    private Vector3 bobAmount;
    private Vector3 bobRotation;

    private float curveSin { get => Mathf.Sin(speedCurve); }
    private float curveCos { get => Mathf.Cos(speedCurve); }
    private Vector3 bobPosition;

    private Quaternion originRotation;
    private Vector3 originPosition;
    private float xMouse;
    private float yMouse;
    // Start is called before the first frame update
    void Start()
    {
        input = transform.root.gameObject.GetComponent<PlayerInput>();
        originRotation = transform.localRotation;
        originPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        SetValue();

        GetMouseValue();

        GetBobRotation();
        GetBobPosition();

        UpdateSway();
        UpdateTiltSway();
    }

    private void GetMouseValue()
    {
        xMouse = -input.mouseInput.x * 0.3f;
        yMouse = -input.mouseInput.y * 0.3f;
    }
    private void UpdateSway()
    {
        float moveX = Mathf.Clamp(xMouse * swayAmount, -swayMaxAmount, swayMaxAmount);
        float moveY = Mathf.Clamp(yMouse * swayAmount, -swayMaxAmount, swayMaxAmount);

        Vector3 finalPosition = new Vector3(moveX, moveY, 0);
        // use lerp because adjust position not rotation
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + originPosition + bobPosition, Time.deltaTime * smooth);
    }
    private void UpdateTiltSway()
    {
        float tiltX = Mathf.Clamp(xMouse * tiltAmount, -tiltAmount, tiltAmount);
        float tiltY = Mathf.Clamp(yMouse * tiltAmount, -tiltAmount, tiltAmount);

        Quaternion targetRotation = Quaternion.Euler(new Vector3(rotationX ? -tiltY : 0f, rotationY ? tiltX : 0f, rotationZ ? tiltX : 0f));
        // use slerp because adjust rotation not position
        transform.localRotation = Quaternion.Slerp(transform.localRotation, originRotation * targetRotation * Quaternion.Euler(bobRotation), Time.deltaTime * smoothRotation);
    }

    private void GetBobPosition()
    {
        bobPosition.x = (curveCos * bobLimit.x * (controller.isGrounded ? 1 : 0)) - (input.moveInput.x * travelLimit.x);
        bobPosition.y = (curveSin * bobLimit.y) - (controller.velocity.y * travelLimit.y);
        bobPosition.z = -(input.moveInput.y * travelLimit.z);
    }

    private void GetBobRotation()
    {
        if (input.moveInput != Vector2.zero)
        {
            speedCurve += Time.deltaTime * (controller.isGrounded ? bobbingSpeedForWalking : 1f);
        }
        else
        {
            speedCurve += Time.deltaTime * (controller.isGrounded ? bobbingSpeedForStanding : 1f);
        }

        bobRotation.x = (input.moveInput != Vector2.zero ? bobAmount.x * (Mathf.Sin(2 * speedCurve)) : bobAmount.x * (Mathf.Sin(2 * speedCurve) /5));
        bobRotation.y = (input.moveInput != Vector2.zero ? bobAmount.y * curveCos : 0);
        bobRotation.z = (input.moveInput != Vector2.zero ? bobAmount.z * curveCos * input.moveInput.x : 0);
    }

    private void SetValue()
    {
        if (input.isAiming == true)
        {
            swayAmount = aimSwayAmount;
            swayMaxAmount = aimSwayMaxAmount;

            tiltAmount = aimTiltAmount;

            travelLimit = aimTravelLimit;
            bobLimit = aimBobLimit;

            bobAmount = aimBobAmount;
        }
        else
        {
            swayAmount = idleSwayAmount;
            swayMaxAmount = idleSwayMaxAmount;


            tiltAmount = idleTiltAmount;

            travelLimit = idleTravelLimit;
            bobLimit = idleBobLimit;

            bobAmount = idleBobAmount;
        }
    }
}