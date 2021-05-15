using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSandwichScript : MonoBehaviour
{
    public GameObject projectile;
    public Transform barrelEnd;
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal);
                //Instantiate(projectile, hit.point, hitObjectRotation);
                Instantiate(projectile, hit.point, hitObjectRotation);
            }
        }
    }
}
