using System;
using System.Collections.Generic;
using UnityEngine;


public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator, cellIndecator;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabase database;
    private int SelectedObjectIndex = -1;

    [SerializeField]
    private GameObject gridVisualization;

    private GridData floorData, BuildingData;

    private Renderer previewRenderer;

    private List<GameObject> placedGameObjects = new();

    private void Start()
    {
        stopPlacement();
        floorData = new();
        BuildingData = new();
        previewRenderer = cellIndecator.GetComponentInChildren<Renderer>();
    }

  
    public void startPlacement(int ID)
    {
       SelectedObjectIndex = database.objects.FindIndex(data => data.ID == ID);
        if (SelectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        gridVisualization.SetActive(true);
        cellIndecator.SetActive(true);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += stopPlacement;
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition, SelectedObjectIndex);
        if (!placementValidity) 
            return;
        GameObject gameObject = Instantiate(database.objects[SelectedObjectIndex].prefab);
        gameObject.transform.position = grid.GetCellCenterWorld(gridPosition);
        placedGameObjects.Add(gameObject);
        GridData selectedData = BuildingData;
        selectedData.AddObjectAt(gridPosition, database.objects[SelectedObjectIndex].Size, database.objects[SelectedObjectIndex].ID, placedGameObjects.Count -1);
    }

   

    private void stopPlacement()
    {
        SelectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        cellIndecator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= stopPlacement;
    }

    private void Update()
    {
        if (SelectedObjectIndex < 0)  
            return; 
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition, SelectedObjectIndex);
        previewRenderer.material.color = placementValidity ? Color.white : Color.red;   

        mouseIndicator.transform.position = mousePosition;
        cellIndecator.transform.position = grid.GetCellCenterWorld(gridPosition);
    }


    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = BuildingData;
        return selectedData.CanPlaceObjectAt(gridPosition, database.objects[selectedObjectIndex].Size);
    }
}