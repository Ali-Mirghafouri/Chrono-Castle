using System;
using System.Collections.Generic;
using UnityEngine;

public class GridData 
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new();

    public void AddObjectAt (Vector3Int gridPosition, Vector2Int objectSize, int ID, int placedObjectIndex)
    {
        List<Vector3Int> posToOccupy = CalculatePos(gridPosition, objectSize);
        PlacementData data = new PlacementData(posToOccupy, ID, placedObjectIndex);
        foreach (var pos in posToOccupy)
        {
            if (placedObjects.ContainsKey(pos)) 
                throw new Exception($"Dictionary already contains this cell position {pos}");
            placedObjects[pos] = data;
        }
    }

    private List<Vector3Int> CalculatePos(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new();
        var offset = new Vector3Int((objectSize.x / 2), 0, objectSize.y / 2);
        for (int i = 0; i < objectSize.x; i++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add((gridPosition - offset) + new Vector3Int(i , 0, y));
               
            }
        }
        return returnVal;  
    }

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> posToOccupy = CalculatePos(gridPosition, objectSize);

        foreach (var pos in posToOccupy)
        {
            if(placedObjects.ContainsKey(pos))
                return false;
        }
        return true;

    }

    internal int GetRepIndex(Vector3Int gridPos)
    {
        if (placedObjects.ContainsKey(gridPos) == false)
            return -1;
        return placedObjects[gridPos].PlacementObjectIndex;
    }

    internal void RemoveObjectAt(Vector3Int gridPos)
    {
        foreach (var pos in placedObjects[gridPos].occupiedPositions)
        {
            placedObjects.Remove(pos);
        }
    }
}

public class PlacementData
{
    public List<Vector3Int> occupiedPositions;
    public int ID {  get; private set; }
    public int PlacementObjectIndex {  get; private set; }


    public PlacementData(List<Vector3Int> occupiedPositions, int iD, int placementObjectIndex)
    {
        this.occupiedPositions = occupiedPositions;
        ID = iD;
        PlacementObjectIndex = placementObjectIndex;
    }
}