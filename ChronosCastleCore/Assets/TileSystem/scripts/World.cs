using System;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public static World current;


    public Dictionary<Vector2, Tile> tiles;

    [SerializeField]
    private ObjectPlacer database;


    private void Start()
    {
        current = this;
        tiles = new Dictionary<Vector2, Tile>();
        GameObject[] AllTiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in AllTiles)
        {
            Tile tempTile = new Tile(tile, new Vector2(tile.transform.position.x, tile.transform.position.z), tile.transform.position);
            tiles.Add(tempTile.GetPos(), tempTile);
        }

    }

    public void AddStructure(Vector3 gridPosition, Vector2Int objectSize, int ID, int placedObjectIndex)
    {

        //GameObject go = database.GetPlacedObjectByID(goIndex);
        //tiles[offset].AddStructure(go);

        List<Vector3> posToOccupy = CalculatePos(gridPosition, objectSize);
        GameObject go = database.GetPlacedObjectByID(placedObjectIndex);

        foreach (var pos in posToOccupy)
        {
        //var offset = new Vector2(pos.x > 0 ? pos.x - 0.5f : pos.x + 0.5f, pos.y > 0 ? pos.y - 0.5f : pos.y + 0.5f);
            if (tiles[new Vector2(pos.x, pos.z)].IsBlocked())
                throw new Exception($"Dictionary already contains this cell position {pos}");
            if (World.current != null)
            {
                //Debug.Log(new Vector2(pos.x, pos.z));
                tiles[new Vector2(pos.x, pos.z)].AddStructure(go, placedObjectIndex);
            }

        }
    }

    public List<Vector3> CalculatePos(Vector3 gridPosition, Vector2Int objectSize)
    {
        List<Vector3> returnVal = new();
        var offset = new Vector3Int((objectSize.x / 2), 0, objectSize.y / 2);
        for (int i = 0; i < objectSize.x; i++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add((gridPosition - offset) + new Vector3(i, 0, y));

            }
        }

        return returnVal;
    }

    public bool CanPlaceObjectAt(Vector3 gridPosition, Vector2Int objectSize)
    {
        List<Vector3> posToOccupy = CalculatePos(gridPosition, objectSize);

        foreach (var pos in posToOccupy)
        {
            //Debug.Log(pos);
            //Debug.Log(tiles.ContainsKey(pos));

            if (!tiles.ContainsKey(new Vector2(pos.x, pos.z)) || tiles[new Vector2(pos.x, pos.z)].IsBlocked())
                return false;
        }
        return true;

    }

    public bool IsTileBlocked(Vector3 pos)
    {
        //Debug.Log(pos);
        return tiles[new Vector2(pos.x, pos.z)].IsBlocked();
    }

    public Tile GetTileByPos(Vector3 pos)
    {
        var xz = new Vector2(pos.x, pos.z);
        return tiles[xz];
    }

    private void OnDestroy()
    {
        current = null;
    }
}
