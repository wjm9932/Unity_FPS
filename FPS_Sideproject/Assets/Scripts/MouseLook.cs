using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Recoil recoil;


    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public Transform rotateBody;
    private float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 10 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 10 * Time.deltaTime;
        
        xRotation -= mouseY;
        
        Vector3 finalXRotation = new Vector3(xRotation, 0f, 0f) + recoil.currentRotation;
        finalXRotation.x = Mathf.Clamp(finalXRotation.x, -90f, 90f);
        
        rotateBody.transform.localRotation = Quaternion.Euler(finalXRotation.x, finalXRotation.y, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
