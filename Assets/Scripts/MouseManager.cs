using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
                
                if(rayHit)
                {
                    Debug.Log(rayHit.transform.name);
                }
            }
        }
    }
}
