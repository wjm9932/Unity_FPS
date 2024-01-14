using UnityEngine;
using UnityEngineInternal;

public class Weapon_M4 : MonoBehaviour
{
    public int damage;
    public int magazineSize;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;

    private int bulletSLeft, bulletsShot;
    private bool isShooting, isReadyToShoot, isReloading;

    private PlayerInput input;
    private Recoil recoil;
    private AdvancedWeaponRecoil advancedWeaponRecoil;

    public ParticleSystem muzzleFlashEffect;
    public ParticleSystem shellEjectEffect;

    //temp
    public GameObject temp;

    public Camera cam;
    public Transform firePos;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    // Start is called before the first frame update
    void Start()
    {
        input = transform.root.gameObject.GetComponent<PlayerInput>();
        advancedWeaponRecoil = GetComponent<AdvancedWeaponRecoil>();
        recoil = GetComponent<Recoil>();

        bulletSLeft = magazineSize;
        isReadyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(input.isReload == true && isReloading == false)
        {
            Reload();
        }
        
    }
    private void FixedUpdate()
    {
        if (input.isFiring == true && isReadyToShoot == true && !isReloading && bulletSLeft > 0)
        {
            Shoot();
        }
    }
    private void Reload()
    {
        isReloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletSLeft = magazineSize;
        isReloading = false;
    }
    private void Shoot()
    {
        muzzleFlashEffect.Play();
        shellEjectEffect.Play();

        recoil.FireCameraRecoil();
        advancedWeaponRecoil.FireWeaponRecoil();

        isReadyToShoot = false;
        --bulletSLeft;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit, range, whatIsEnemy) == true)
        {
            var target = rayHit.collider.GetComponent<IDamageble>();
            if (target != null)
            {
                DamageMessage damageMessage;
                damageMessage.amount = damage;
                damageMessage.hitPoint = rayHit.point;
                damageMessage.hitNormal = rayHit.normal;

                target.ApplyDamage(damageMessage);
            }
            else
            {
                EffectManager.Instance.PlayHitEffect(rayHit.point, rayHit.normal, rayHit.transform);
            }
        }

        Invoke("ResetShot", timeBetweenShooting);
    }
    private void ResetShot()
    {
        isReadyToShoot = true;
    }
}
