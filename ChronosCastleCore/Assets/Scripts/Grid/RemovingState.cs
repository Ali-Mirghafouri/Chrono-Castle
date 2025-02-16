using UnityEngine;

public class RemovingState : IBuildingState
{
    //private int gameObjectIndex = -1;
    Grid grid;
    PreviewSystem previewSystem;
    ObjectPlacer ObjectPlacer;

    public RemovingState(Grid grid, PreviewSystem previewSystem, ObjectPlacer objectPlacer)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;
        ObjectPlacer = objectPlacer;

        previewSystem.StartShowingRemovePreview();
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }
    // change to be able to remove shit
    public void OnAction(Vector3 gridPos)
    {
        ObjectPlacer.RemoveObjectAt(World.current.GetTileByPos((gridPos)));
        previewSystem.UpdatePos(gridPos, false);

    }

    public void UpdateState(Vector3 gridPos)
    {
        previewSystem.UpdatePos(gridPos, !World.current.IsTileBlocked(gridPos));
    }
}
