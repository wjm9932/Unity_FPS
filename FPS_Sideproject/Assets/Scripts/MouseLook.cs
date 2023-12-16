using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Recoil recoil;


    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public Transform rotateBody;

    private PlayerInput input;
    private float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = input.mouseInput.x * mouseSensitivity * 10 * Time.deltaTime;
        float mouseY = input.mouseInput.y * mouseSensitivity * 10 * Time.deltaTime;

        xRotation -= mouseY;

        Vector3 finalRotation = new Vector3(xRotation, 0f, 0f) + recoil.currentRotation;
        finalRotation.x = Mathf.Clamp(finalRotation.x, -90f, 90f);

        if (input.isFiring == true)
        {
            rotateBody.transform.localRotation = Quaternion.Lerp(rotateBody.transform.localRotation, Quaternion.Euler(finalRotation.x, finalRotation.y, 0f), Time.deltaTime * 50f);
        }
        else
        {
            rotateBody.transform.localRotation = Quaternion.Euler(finalRotation.x, finalRotation.y, 0f);
        }
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
