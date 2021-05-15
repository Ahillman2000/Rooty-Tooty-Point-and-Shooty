using UnityEngine;

public class FireSandwichScript : MonoBehaviour
{
    public GameObject projectile;
    public Transform barrelEnd;

    public float force = 20f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject _projectile = Instantiate(projectile, barrelEnd.transform.position, Quaternion.Euler(barrelEnd.transform.forward));
        _projectile.GetComponent<Rigidbody>().AddForce(barrelEnd.transform.forward * force, ForceMode.Impulse);
        Destroy(_projectile, 5f);
    }
}
