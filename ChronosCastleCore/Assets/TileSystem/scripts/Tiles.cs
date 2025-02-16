using System;
using UnityEngine;

[Serializable]
public class Tile 
{

    private GameObject current;

    private GameObject Structure;
    private int StructureIndex = -1;

    private Vector2 Pos;
    private Vector3 WorldPos;


    public Tile(GameObject tile, Vector2 pos, Vector3 worldPos)
    {
        current = tile;
        Pos = pos;
        WorldPos = worldPos;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool IsBlocked()
    {
        return Structure != null;
    }

    public Vector2 GetPos()
    {
        return Pos;
    }

    public int GetStructureIndex()
    {
        return StructureIndex;
    }

    public Vector3 GetWorldPos()
    {
        return WorldPos;
    }

    public GameObject GetStructure()
    {
        return Structure;
    }

    public GameObject GetCurrent()
    {
        return current;
    }

    public void AddStructure(GameObject go, int index)
    {
        this.Structure = go;
        this.StructureIndex = index;
    }
    public void RemoveStructure()
    {
        this.Structure = null;
        this.StructureIndex = -1;
    }
}
