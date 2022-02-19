using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public Animator animator;

    public bool automatic;
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    float nextTimeToFire = 0f;

    public int maxAmmo;
    private int currentAmmoCapacity;
    public float reloadTime;
    private bool isReloading = false;

    public Camera playerCamera;
    public GameObject bulletSpawn;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffectTerrain;

    private void Start()
    {
        currentAmmoCapacity = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void Update()
    {
        if(isReloading)
        {
            return;
        }

        if(currentAmmoCapacity == 0 || ((currentAmmoCapacity < maxAmmo) && Input.GetKey(KeyCode.R)))
        {
            StartCoroutine(Reload());
            return;
        }
        if(automatic)
        {
            if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        currentAmmoCapacity--;

        RaycastHit hit;
        if(Physics.Raycast(bulletSpawn.transform.position, bulletSpawn.transform.forward, out hit, range))
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * range, Color.red, 5f);
            Debug.Log(hit.transform.name);

            Player playerStats = hit.transform.GetComponent<Player>();
            if(playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }
            
            GameObject _impactDecalTerrain = Instantiate(impactEffectTerrain, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(_impactDecalTerrain, 2.5f);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading");
        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        currentAmmoCapacity = maxAmmo;
        isReloading = false;
    }
}
