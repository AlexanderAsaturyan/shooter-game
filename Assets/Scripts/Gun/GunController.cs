using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem flareEffect;

    private float damage = 10f;
    private float range = 100f;
    private float impactForce = 50f;
    private float fireRate = 15;
    private float nextTimeToFire = 0;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        
        RaycastHit hit;
        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
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