using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 moveInput { get; private set; }
    public Vector2 mouseInput { get; private set; }
    public bool isJumping { get; private set; }
    public bool isFiring { get; private set; }
    public bool isFiringEnd { get; private set; }
    public bool isAiming { get; private set; }
    public bool isReload { get; private set; }
    //public bool isReload { get; private set; }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (moveInput.sqrMagnitude > 1f)
        {
            moveInput = moveInput.normalized;
        }
        isJumping = Input.GetButtonDown("Jump");
        isFiring = Input.GetButton("Fire1");
        isFiringEnd = Input.GetButtonUp("Fire1");
        isAiming = Input.GetButton("Fire2");
        isReload = Input.GetButtonDown("Reload");
    }
}
