using UnityEngine;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    int ID;
    Grid grid;
    PreviewSystem previewSystem;
    ObjectsDatabase database;
    ObjectPlacer ObjectPlacer;

    public PlacementState(int iD, Grid grid, PreviewSystem previewSystem, ObjectsDatabase database, ObjectPlacer objectPlacer)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
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

    public void OnAction(Vector3 gridPos)
    {
        //Debug.Log(pos);

        bool placementValidity = CheckPlacementValidity(gridPos, selectedObjectIndex);
        if (!placementValidity)
            return;
        int index = ObjectPlacer.PlaceObject(database.objects[selectedObjectIndex].prefab, gridPos);

        World.current.AddStructure(gridPos, database.objects[selectedObjectIndex].Size, database.objects[selectedObjectIndex].ID, index);
        previewSystem.UpdatePos(gridPos, false);
    }

    private bool CheckPlacementValidity(Vector3 gridPosition, int selectedObjectIndex)
    {
        return World.current.CanPlaceObjectAt(gridPosition, database.objects[selectedObjectIndex].Size);
    }

    public void UpdateState(Vector3 gridPos)
    {
        bool placementValidity = CheckPlacementValidity(gridPos, selectedObjectIndex);
        previewSystem.UpdatePos(gridPos, placementValidity);
    }
}
