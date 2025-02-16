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

    internal void RemoveObjectAt(int gameObjectIndex)
    {
        if (placedGameObjects.Count <= gameObjectIndex || placedGameObjects[gameObjectIndex] == null)
        {
            return;
        }
        Destroy(placedGameObjects[gameObjectIndex]);
        placedGameObjects[gameObjectIndex] = null;
    }

    public GameObject GetPlacedObjectByID (int gameObjectIndex)
    {
        GameObject goToReturn = placedGameObjects[gameObjectIndex];
        if(goToReturn == null) 
        {
            Debug.Log("wtf bro");
            return null;
        }
        else 
        {
            return placedGameObjects[gameObjectIndex];
        }        
    }
}
