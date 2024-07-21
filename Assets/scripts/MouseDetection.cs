using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetection : MonoBehaviour
{
    public Grid ground;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Clicking
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldSpace = cam.ScreenToWorldPoint(mousePos);
            Vector3Int gridSpace = ground.WorldToCell(worldSpace);
            Debug.Log(gridSpace);
            

         
        }
    }

}