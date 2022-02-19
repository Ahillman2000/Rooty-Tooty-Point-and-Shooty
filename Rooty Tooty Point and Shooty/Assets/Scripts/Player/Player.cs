using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public GameObject playerBody;
    Material playerBodyMaterial;

    public enum Teams {GREEN, RED};
    public Teams playerTeam;

    void Start()
    {
        GenerateRandomBodyColor();
        currentHealth = maxHealth;
    }

    private void GenerateRandomBodyColor()
    {
        playerBodyMaterial = playerBody.GetComponent<Renderer>().material;

        byte R = (byte)Random.Range(0, 255);
        byte G = (byte)Random.Range(0, 255);
        byte B = (byte)Random.Range(0, 255);
        byte A = 255;

        Color32 randomColor = new Color32(R, G, B, A);
        playerBodyMaterial.color = randomColor;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
