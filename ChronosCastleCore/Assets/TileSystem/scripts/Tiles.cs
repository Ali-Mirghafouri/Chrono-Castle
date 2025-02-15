using System;
using UnityEngine;

[Serializable]
public class Tile 
{

    private GameObject Structure;

    private Vector2 Pos;
    private Vector3 WorldPos;

    public Tile(GameObject structure, Vector2 pos, Vector3 worldPos)
    {
        Structure = structure;
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

    public Vector3 GetWorldPos()
    {
        return WorldPos;
    }

    public GameObject GetStructure()
    {
        return Structure;
    }

    public void AddStructure(GameObject go)
    {
        this.Structure = go;
    }
    public void RemoveStructure()
    {
        this.Structure = null;
    }
}
