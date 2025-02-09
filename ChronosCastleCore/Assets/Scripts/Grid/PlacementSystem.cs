using System;
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

    private void Start()
    {
        stopPlacement();
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
        GameObject gameObject = Instantiate(database.objects[SelectedObjectIndex].prefab);
        gameObject.transform.position = grid.GetCellCenterWorld(gridPosition);
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
        mouseIndicator.transform.position = mousePosition;
        cellIndecator.transform.position = grid.GetCellCenterWorld(gridPosition);
    }
}