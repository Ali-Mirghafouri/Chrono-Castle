using UnityEngine;


public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator, cellIndecator;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    private Vector3 Test;

    private void Update()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        mouseIndicator.transform.position = mousePosition;
        cellIndecator.transform.position = grid.GetCellCenterLocal(gridPosition);
    }
}