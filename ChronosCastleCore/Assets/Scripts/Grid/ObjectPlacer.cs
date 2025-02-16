using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> placedGameObjects = new();

    public int PlaceObject(GameObject prefab, Vector3 pos)
    {
        GameObject gameObject = Instantiate(prefab);
        gameObject.transform.position = pos;
        placedGameObjects.Add(gameObject);
        return placedGameObjects.Count - 1;
    }

    internal void RemoveObjectAt(Tile tile)
    {
        if (tile == null)
            return;
        Destroy(placedGameObjects[tile.GetStructureIndex()]);
        placedGameObjects[tile.GetStructureIndex()] = null;
        tile.RemoveStructure();
    }

    public GameObject GetPlacedObjectByID (int gameObjectIndex)
    {
        GameObject goToReturn = placedGameObjects[gameObjectIndex];
            return placedGameObjects[gameObjectIndex];
    }
}
