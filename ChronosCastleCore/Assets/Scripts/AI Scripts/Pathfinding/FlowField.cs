using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;

public class FlowField : MonoBehaviour
{
    public static FlowField current;
    public Dictionary<Vector2,int> CostfieldScore; //<-- using byte instead of the tile itself to denote it's score.

    Tile TargetTile;
    Tile EnemyTargetTile;

    private void Start()
    {
        CostfieldScore = new Dictionary<Vector2, int>();
        current = this;
    }

    private void OnDestroy()
    {
        current = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            CreateCostField();
        }
    }
    public void GenerateFlowField() 
    {

        foreach(var kvp in World.current.tiles) 
        {
            
        }
        
    }

    public int GetTileCost(Vector2 tilePos) 
    {
        if(CostfieldScore == null) 
        {
            Debug.LogError("The cost field score does not exit. Kindly initialize the CostfieldScore dictionary first.");
            return 0;
        }
        
        if(CostfieldScore.TryGetValue(tilePos, out int result))
        {
            return result;
        }
        else 
        {
            Debug.LogError("Tile does not exit at position : " + tilePos.x + ", " + tilePos.y);
            return 0;
        }
    }

    public void IncreaseTileCost(Vector2 tilePos, int amount) 
    {
        
        if (CostfieldScore == null)
        {
            Debug.LogError("The cost field score does not exit. Kindly initialize the CostfieldScore dictionary first.");
            return;
        }

        if (World.current == null)
        {
            Debug.LogError("World.current cannot be null, otherwise no flow field will be generated");
            return;
        }

        if (CostfieldScore.ContainsKey(tilePos))
        {
            if (GetTileCost(tilePos) >= 255) { Debug.Log("Tile is already at max value!"); return; }

            if (amount + GetTileCost(tilePos) <= 255)
            {
                CostfieldScore[tilePos] = GetTileCost(tilePos) + amount;
            }
            else 
            {
                CostfieldScore[tilePos] = 255; 
            }
        }
        else 
        {
            Debug.LogError("Tile does not exit at position : " + tilePos.x + ", " + tilePos.y);
            return;
        }
    }

    private void AddElementToDict(Vector2 tilePos, int cost) 
    {
        if(CostfieldScore == null) 
        {
            Debug.LogError("CostfieldScore can't be null dumbass");
            return;
        }

        CostfieldScore.Add(tilePos, cost);
    }

    //initial cost increase
    public void CreateCostField() 
    {

        if (World.current == null)
        {
            Debug.LogError("World.current cannot be null, otherwise no flow field will be generated");
            return;
        }

        if (World.current.tiles == null)
        {
            Debug.LogError("World.current.tiles cannot be null, kindly initialize the tiles first before initializing the flow field.");
            return;
        }

        foreach (var kvp in World.current.tiles) 
        {
            switch (kvp.Value.GetTileType()) 
            {
                case TileType.Grass:
                    AddElementToDict(kvp.Key, 2);
                    break;

                case TileType.Road:
                    AddElementToDict(kvp.Key, 1);
                    break;

                case TileType.Lava:
                    AddElementToDict(kvp.Key, 255);
                    break;

                default:
                    AddElementToDict(kvp.Key, 0);
                    break;
            }
            
        }

        Debug.Log("Costfield successfully initialized");
    }

    private void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleCenter;
        if (World.current == null) { return; }
        if(World.current.tiles.Count <= 0) { return; }
        if(CostfieldScore != null) 
        {
            foreach(var kvp in CostfieldScore) 
            {
                Vector3 worldPos = new Vector3(kvp.Key.x, 0, kvp.Key.y);
                Handles.Label(worldPos, kvp.Value.ToString(), style);
            }
        }
    }
}
