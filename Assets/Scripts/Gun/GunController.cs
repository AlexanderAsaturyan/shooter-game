using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem flareEffect;

    WaitForSeconds waitForOneSecond = new WaitForSeconds(1);

    private float damage = 10f;
    private float range = 100f;
    private float impactForce = 70f;
    private float fireRate = 15;
    private float nextTimeToFire = 0;

    private float bulletsCount = 30f;
    public float BulletCount => bulletsCount;

    private bool canShoot = true;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;

            if(bulletsCount >= 1)
            {
                canShoot = true;
            }
            else
            {
                canShoot = false;
            }

            if(canShoot)
            {
                Shoot();            
                StopAllCoroutines();
            }
            else
            {
                StartCoroutine(Reload());
            }
        }

        Debug.LogWarning(bulletsCount);
    }

    private IEnumerator Reload()
    {
        while (true)
        {
            yield return waitForOneSecond;
            bulletsCount = 30f;
        }
    }
    
    private void Shoot()
    {
        bulletsCount--;
        muzzleFlash.Play();
        
        RaycastHit hit;
        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            TargetController target = hit.transform.GetComponent<TargetController>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            ParticleSystem flareGO = Instantiate(flareEffect, hit.point, Quaternion.LookRotation(hit.normal));
            flareGO.Play();
            Destroy(flareGO.gameObject, 2f);
        }
    }
}