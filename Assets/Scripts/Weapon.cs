  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera playerCamera;

    //shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;


    // burst
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    //spread
    public float spreadIntensity;

    //bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletLifetime = 3f;

    public enum ShootingMode
    {
         Single,
         Burst,
         Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
    }
    void Update()
    {
        if (currentShootingMode  == ShootingMode.Auto)
        {
            // holding down left mouse button
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
            // clicking left mouse button once
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if(readyToShoot && isShooting)
        {
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;
        
        // instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.transform.forward = shootingDirection;

        // shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);

        // destroy the bullet after a while
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifetime));

        // check if we are doneshooting
        if(allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        //burst mode
        if(currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1) // we already shoot once before this check
        {
            burstBulletsLeft --;
            Invoke("FireWeapon", shootingDelay);
        }

    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }
     
    public Vector3 CalculateDirectionAndSpread()
    {
        // shooting from the middle of the screen to check where we are pointing at
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            // hitting something
            targetPoint = hit.point;
        }
        else
        {
            // shooting in the air
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

    }
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }






}
