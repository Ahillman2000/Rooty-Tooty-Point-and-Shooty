using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandwich : MonoBehaviour
{
    public float damage = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Stats enemyStats = collision.gameObject.GetComponent<Stats>();
            enemyStats.TakeDamage(damage);
        }
    }
}
