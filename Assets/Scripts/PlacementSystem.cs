using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;

    [SerializeField]
    private MouseDetection inputManager;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabaseS0 database;
    private int selectedObjectIndex = -1;

    private GridData furnitureData;

    private Renderer previewRenderer;

    private List<GameObject> placedGameObject = new();

   // [SerializeField]
    //private GameObject gridVisualization;

    private void Start()
    {
        StopPlacement();
        furnitureData = new();
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }
    public void StartPlacement(int ID)
    {
        Debug.Log("start placement!");
        StopPlacement();
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if(selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID Found {ID}");
            return;
        }
        //gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if(inputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        bool placementValidity = CheckPlacementValidity(gridPos, selectedObjectIndex);
        if (placementValidity == false)
            return;

        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        Vector3 spritePos = grid.CellToWorld(gridPos);
        spritePos.y += database.objectsData[selectedObjectIndex].YOffset;
        newObject.transform.position = spritePos;
        placedGameObject.Add(newObject);
        GridData selectedData = furnitureData;
        selectedData.AddObjectAt(gridPos,
            database.objectsData[selectedObjectIndex].Size,
            database.objectsData[selectedObjectIndex].ID,
            placedGameObject.Count - 1);
    }

    private bool CheckPlacementValidity(Vector3Int gridPos, int selectedObjectIndex)
    {
        GridData selectedData = furnitureData;

        return selectedData.CanPlaceObjectAt(gridPos, database.objectsData[selectedObjectIndex].Size);
    }


    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        //gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    private void Update()
    {
       if (selectedObjectIndex < 0)
        {
         return; 
        }
        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        bool placementValidity = CheckPlacementValidity(gridPos, selectedObjectIndex);
        previewRenderer.material.color = placementValidity ? Color.white : Color.red;


        mouseIndicator.transform.position = mousePos;
        Vector3 snappedPos = grid.CellToWorld(gridPos);
        snappedPos.y += 0.25f; 
        cellIndicator.transform.position = snappedPos; 
        
    }
}
