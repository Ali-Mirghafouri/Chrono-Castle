using System.Collections.Generic;
using UnityEngine;
using System;



[Serializable]
public class ObjectData
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public Vector2Int Size { get; private set; } = Vector2Int.one;
    [field: SerializeField]
    public GameObject prefab { get; private set; }
}


[CreateAssetMenu(fileName = "ObjectsDatabase", menuName = "Scriptable Objects/ObjectsDatabase")]
public class ObjectsDatabase : ScriptableObject
{
    public List<ObjectData> objects;
}



