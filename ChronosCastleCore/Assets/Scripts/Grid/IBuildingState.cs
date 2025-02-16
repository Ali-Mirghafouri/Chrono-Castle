using UnityEngine;

public interface IBuildingState
{
    void EndState();
    void OnAction(Vector3 gridPos);
    void UpdateState(Vector3 gridPos);
}