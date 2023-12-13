using UnityEngine;

public class Weapon_M4 : MonoBehaviour
{
    public int Damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize;

    private int bulletSLeft, bulletsShot;
    private bool isShooting, isReadyToShoot, isReloading;
    private PlayerInput input;

    public GameObject temp;

    public Camera cam;
    public Transform firePos;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    // Start is called before the first frame update
    void Start()
    {
        bulletSLeft = magazineSize;
        isReadyToShoot = true;
        input = transform.root.gameObject.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if(input.isReload == true && isReloading == false)
        {
            Reload();
        }
        if(input.isFiring == true && isReadyToShoot == true && !isReloading && bulletSLeft > 0)
        {
            Shoot();
        }
    }
    private void Reload()
    {
        isReloading = true;
        Invoke("reloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletSLeft = magazineSize;
        isReloading = false;
    }
    private void Shoot()
    {
        isReadyToShoot = false;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit, range, whatIsEnemy) == true)
        {
            Debug.Log(rayHit.collider.name);
        }

        Instantiate(temp, rayHit.point, Quaternion.identity);

        --bulletSLeft;
        Invoke("ResetShot", timeBetweenShooting);
    }
    private void ResetShot()
    {
        isReadyToShoot = true;
    }
}
