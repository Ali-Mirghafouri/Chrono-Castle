using System;
using System.Collections.Generic;
using UnityEngine;


public class PlacementSystem : MonoBehaviour
{
    
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabase database;
  

    [SerializeField]
    private GameObject gridVisualization;

    private GridData floorData, BuildingData;

    [SerializeField]
    private PreviewSystem preview;

    [SerializeField]
    private ObjectPlacer objectPlacer;

    private Vector3Int lastDetectedPos = Vector3Int.zero;

    IBuildingState buildingState;

    private void Start()
    {
        stopPlacement();
        floorData = new();
        BuildingData = new();
    }

  
    public void startPlacement(int ID)
    {
        stopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new PlacementState(ID, grid, preview, database, BuildingData, objectPlacer);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += stopPlacement;
    }

    public void StartRemoving()
    {
        stopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new RemovingState(grid, preview, BuildingData, objectPlacer);
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

        buildingState.OnAction(gridPosition);

    }

   

    private void stopPlacement()
    {
        gridVisualization.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= stopPlacement;
        lastDetectedPos = Vector3Int.zero;
        if (buildingState == null)
            return;
        buildingState.EndState();
        buildingState = null;   
    }

    private void Update()
    {
        if (buildingState == null)  
            return; 
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if (lastDetectedPos != gridPosition)
        {
            buildingState.UpdateState(gridPosition);
            lastDetectedPos = gridPosition;
        }
            
      
    }


}