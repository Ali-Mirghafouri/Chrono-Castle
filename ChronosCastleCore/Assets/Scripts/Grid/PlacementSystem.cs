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
  

    public GameObject[] gridVisualization;

    [SerializeField]
    private PreviewSystem preview;

    [SerializeField]
    private ObjectPlacer objectPlacer;

    private Vector3Int lastDetectedPos = Vector3Int.zero;

    IBuildingState buildingState;

    private void Start()
    {
        gridVisualization = GameObject.FindGameObjectsWithTag("Grid");
        VisToggle(false);
    }


    public void startPlacement(int ID)
    {
        stopPlacement();
        VisToggle(true);

        buildingState = new PlacementState(ID, grid, preview, database, objectPlacer);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += stopPlacement;
    }


    public void StartRemoving()
    {
        stopPlacement();
        VisToggle(true);

        buildingState = new RemovingState(grid, preview, objectPlacer);
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
        //Debug.Log(grid.GetCellCenterWorld(gridPosition));
        buildingState.OnAction(grid.GetCellCenterWorld(gridPosition));

    }

   

    private void stopPlacement()
    {
        VisToggle(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= stopPlacement;
        lastDetectedPos = Vector3Int.zero;
        if (buildingState == null)
            return;
        buildingState.EndState();
        buildingState = null;   
    }

    public void VisToggle(Boolean active)
    {
        foreach (GameObject go in gridVisualization)
        {
            go.SetActive(active);
        }
    }

    private void Update()
    {
        if (buildingState == null)  
            return; 
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if (lastDetectedPos != gridPosition)
        {
            buildingState.UpdateState(grid.GetCellCenterWorld( gridPosition));
            lastDetectedPos = gridPosition;
        }
            
      
    }


}