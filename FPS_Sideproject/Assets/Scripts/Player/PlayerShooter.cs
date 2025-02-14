using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerShooter : MonoBehaviour
{
    private Animator animator;
    
    private PlayerInput input;
    private Weapon gun;
    private void Awake()
    {
        gun = GameObject.FindWithTag("Gun").GetComponent<Weapon>();
        if (gun == null)
        {
            Debug.LogError("Gun with tag 'gun' not found");
        }
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        
    }

    void Update()
    {
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

    private void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);


        animator.SetIKPosition(AvatarIKGoal.LeftHand, gun.leftHandMount.position);
        animator.SetIKPosition(AvatarIKGoal.RightHand, gun.rightHandMount.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, gun.rightHandMount.rotation);
    }
}
