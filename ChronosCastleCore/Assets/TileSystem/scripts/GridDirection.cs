using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GridDirection
{
    //this class is solely to keep track of the direction of the tiles in the World.

    public readonly Vector2Int vector;

    private GridDirection(int x, int y)
    {
        vector = new Vector2Int(x, y);
    }

    public static implicit operator Vector2Int(GridDirection direction) { return direction.vector; }

    
    public static GridDirection GetDirectionFromV2I(Vector2Int vector)
    {
        return CardinalAndIntercardinalDirections.DefaultIfEmpty(None).FirstOrDefault(direction => direction == vector);
    }

    //==Keys to denote grid direction==//
    public static readonly GridDirection None = new GridDirection(0, 0);

    public static readonly GridDirection North = new GridDirection(0, 1);
    public static readonly GridDirection South = new GridDirection(0, -1);
    public static readonly GridDirection East = new GridDirection(1, 0);
    public static readonly GridDirection West = new GridDirection(-1, 0);

    public static readonly GridDirection NorthEast = new GridDirection(1, 1);
    public static readonly GridDirection NorthWest = new GridDirection(-1, 1);
    public static readonly GridDirection SouthEast = new GridDirection(1, -1);
    public static readonly GridDirection SouthWest = new GridDirection(-1, -1);

    public static readonly List<GridDirection> CardinalDirections = new List<GridDirection>
    {
        North,
        South,
        East,
        West
    };

    public static readonly List<GridDirection> CardinalAndIntercardinalDirections = new List<GridDirection>
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    };

    public static readonly List<GridDirection> AllDirections = new List<GridDirection>()
    {
        None,
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    };

}
