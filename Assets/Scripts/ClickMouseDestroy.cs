using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMouseDestroy : MonoBehaviour
{
    public RandomPosition randomPosition;
    public GameObject CubeCollider;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject != CubeCollider)
                {
                    Destroy(hit.collider.gameObject);
                    randomPosition.spheres.Remove(hit.collider.gameObject);
                    randomPosition.cubes.Remove(hit.collider.gameObject);
                    break;
                }
            }
        }
    }
}
