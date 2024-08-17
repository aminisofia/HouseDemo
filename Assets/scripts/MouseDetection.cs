using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class MouseDetection : MonoBehaviour
{
    public Grid ground;
    private Camera cam;
    public Tilemap tilemap;
    private Vector3 lastPosition;
    public Tile woodFloor;
    public event Action OnClicked, OnExit;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        // Clicking
       if (Input.GetMouseButtonDown(0))
        OnClicked?.Invoke();
        //tilemap.SetTile(gridSpace, woodFloor);

       if(Input.GetKeyDown(KeyCode.Escape))
        OnExit?.Invoke();
    }

    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();
  
    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldSpace = cam.ScreenToWorldPoint(mousePos);
        //Vector3Int gridSpace = ground.WorldToCell(worldSpace);
        //gridSpace.z = 0;
        worldSpace.z = 0;
        lastPosition = worldSpace;
        return lastPosition;
    }
}