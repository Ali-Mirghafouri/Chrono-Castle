using UnityEngine;

public class RemovingState : IBuildingState
{
    private int gameObjectIndex = -1;
    Grid grid;
    PreviewSystem previewSystem;
    GridData building;
    ObjectPlacer ObjectPlacer;

    public RemovingState(Grid grid, PreviewSystem previewSystem, GridData building, ObjectPlacer objectPlacer)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.building = building;
        ObjectPlacer = objectPlacer;

        previewSystem.StartShowingRemovePreview();
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPos)
    {
        GridData selectedData = null;
        if (building.CanPlaceObjectAt(gridPos, Vector2Int.one) ==false)
        {
            selectedData = building;
        }
        if (selectedData == null)
            return;
        gameObjectIndex = selectedData.GetRepIndex(gridPos);
        if (gameObjectIndex == -1)
            return;
        selectedData.RemoveObjectAt(gridPos);
        ObjectPlacer.RemoveObjectAt(gameObjectIndex);

        Vector3 cellPos = grid.GetCellCenterWorld(gridPos);
        previewSystem.UpdatePos(cellPos, false);

    }

    public void UpdateState(Vector3Int gridPos)
    {
        bool valid = building.CanPlaceObjectAt(gridPos, Vector2Int.one);
        previewSystem.UpdatePos(grid.GetCellCenterWorld(gridPos), valid);
    }
}
