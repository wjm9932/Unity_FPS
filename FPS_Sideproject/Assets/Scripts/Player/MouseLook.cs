using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    public Transform rotateBody;
    public float mouseSensitivity;

    private PlayerInput input;
    private ScreenRecoil recoil;
    private float xRotation = 0f;

    private bool test = false;
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject gunObject = GameObject.FindWithTag("Gun");
        if (gunObject != null)
        {
            recoil = gunObject.GetComponent<ScreenRecoil>();
        }
        else
        {
            Debug.LogError("Gun with tag 'gun' not found or it doesn't have a ScreenRecoil component!");
        }
    }

    void Start()
    {
        input = GetComponent<PlayerInput>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = input.mouseInput.x * mouseSensitivity;
        float mouseY = input.mouseInput.y * mouseSensitivity;

        xRotation -= mouseY;

        Vector3 finalRotation = new Vector3(xRotation, 0f, 0f) + recoil.currentRotation;
        finalRotation.x = Mathf.Clamp(finalRotation.x, -90f, 90f);

        if (input.isFiring == true)
        {
            rotateBody.transform.localRotation = Quaternion.Slerp(rotateBody.transform.localRotation, Quaternion.Euler(finalRotation.x, finalRotation.y, 0f), Time.deltaTime * 50f);
        }
        else
        {
            rotateBody.transform.localRotation = Quaternion.Euler(finalRotation.x, finalRotation.y, 0f);
        }

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
