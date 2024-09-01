using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerShooter : MonoBehaviour
{
    private PlayerInput input;
    private Weapon gun;
    private void Awake()
    {
        gun = GameObject.FindWithTag("Gun").GetComponent<Weapon>();
        if (gun == null)
        {
            Debug.LogError("Gun with tag 'gun' not found");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        
    }

    void Update()
    {
        Debug.Log(input.isReload);
        if (input.isReload == true && gun.isReloading == false)
        {
            gun.Reload();
        }

    }
    private void FixedUpdate()
    {
        if (input.isFiring == true && gun.isReadyToShoot == true && !gun.isReloading)
        {
            gun.Shoot();
        }
    }
}
