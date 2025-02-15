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


    public void SetStructure(int goIndex, Vector3 pos)
    {
        
        GameObject go = database.GetPlacedObjectByID(goIndex);
        var offset = new Vector2(pos.x > 0 ? pos.x - 0.5f : pos.x + 0.5f, pos.y > 0 ? pos.y - 0.5f : pos.y + 0.5f);
        tiles[offset].SetStructure(go);
        Tile newTile;
        if (tiles.TryGetValue(offset, out newTile))
        {
            Debug.Log(newTile.IsBlocked());
        };
    }









    private void OnDestroy()
    {
        current = null;
    }
}
