using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager:MonoBehaviour
{

    private GameObject item;

    Ray ray;
    RaycastHit hit;

    void Update()
    {
        if(!Camera.main)
        {
            Debug.LogWarning("Main Camera not found!");
            return;
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {

            if(hit.collider.tag.Equals("Item") && Input.GetMouseButtonDown(0))
            {
                hit.transform.SendMessageUpwards("FlipHorizontal");

            }
        }
    }
}
