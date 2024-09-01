using UnityEngine;
using UnityEngineInternal;

public class Weapon : MonoBehaviour
{
    public int damage;
    public int magazineSize;
    public float timeBetweenShooting, range, reloadTime;

    public int bulletSLeft { get; private set; }
    public bool isReadyToShoot { get; private set; }
    public bool isReloading { get; private set; }


    private ScreenRecoil recoil;
    private WeaponRecoil advancedWeaponRecoil;

    public ParticleSystem muzzleFlashEffect;
    public ParticleSystem shellEjectEffect;

    public Transform firePos;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    // Start is called before the first frame update
    void Start()
    {
        advancedWeaponRecoil = GetComponent<WeaponRecoil>();
        recoil = GetComponent<ScreenRecoil>();

        bulletSLeft = magazineSize;
        isReadyToShoot = true;
    }

    public void Reload()
    {
        Debug.Log("Reload Start");

        isReloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        Debug.Log("Reload Finished");

        bulletSLeft = magazineSize;
        isReloading = false;
    }
    public void Shoot()
    {
        muzzleFlashEffect.Play();
        shellEjectEffect.Play();

        recoil.FireCameraRecoil();
        advancedWeaponRecoil.FireWeaponRecoil();

        isReadyToShoot = false;
        --bulletSLeft;

        if(Physics.Raycast(firePos.transform.position, firePos.transform.forward, out rayHit, range, whatIsEnemy) == true)
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
