using UnityEngine;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    int ID;
    Grid grid;
    PreviewSystem previewSystem;
    ObjectsDatabase database;
    GridData building;
    ObjectPlacer ObjectPlacer;

    public PlacementState(int iD, Grid grid, PreviewSystem previewSystem, ObjectsDatabase database, GridData building, ObjectPlacer objectPlacer)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
        this.building = building;
        ObjectPlacer = objectPlacer;

        selectedObjectIndex = database.objects.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(database.objects[selectedObjectIndex].prefab,
            database.objects[selectedObjectIndex].Size);
        }
        else
        {
            throw new System.Exception($"No Object with ID {iD}");
        }
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPos)
    {
        bool placementValidity = CheckPlacementValidity(gridPos, selectedObjectIndex);
        if (!placementValidity)
            return;

        int index = ObjectPlacer.PlaceObject(database.objects[selectedObjectIndex].prefab, grid.GetCellCenterWorld(gridPos));


        GridData selectedData = building;
        selectedData.AddObjectAt(gridPos, database.objects[selectedObjectIndex].Size, database.objects[selectedObjectIndex].ID, index);
        previewSystem.UpdatePos(grid.GetCellCenterWorld(gridPos), false);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = building;
        return selectedData.CanPlaceObjectAt(gridPosition, database.objects[selectedObjectIndex].Size);
    }

    public void UpdateState(Vector3Int gridPos)
    {
        bool placementValidity = CheckPlacementValidity(gridPos, selectedObjectIndex);
        previewSystem.UpdatePos(grid.GetCellCenterWorld(gridPos), placementValidity);
    }
}
