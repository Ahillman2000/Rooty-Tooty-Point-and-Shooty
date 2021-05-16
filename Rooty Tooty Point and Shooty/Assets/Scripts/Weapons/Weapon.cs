using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool automatic;
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    Camera mainCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffectTerrain;

    float nextTimeToFire = 0f;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
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

        RaycastHit hit;
        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range))
        {
            Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * range, Color.red, 5f);
            Debug.Log(hit.transform.name);

            Stats enemyStats = hit.transform.GetComponent<Stats>();
            if(enemyStats != null)
            {
                enemyStats.TakeDamage(damage);
            }
            
            GameObject _impactDecalTerrain = Instantiate(impactEffectTerrain, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(_impactDecalTerrain, 10f);
        }
    }
}
