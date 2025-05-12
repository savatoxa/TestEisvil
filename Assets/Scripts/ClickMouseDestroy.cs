using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMouseDestroy : MonoBehaviour
{
    public RandomPosition randomPosition;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Destroy(hit.collider.gameObject);
                randomPosition.spheres.Remove(hit.collider.gameObject);
                randomPosition.cubes.Remove(hit.collider.gameObject);
            }
        }
    }
}
