using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeReference]
    private Camera sceneCamera;

    private Vector3 Lastposition;

    [SerializeReference]
    private LayerMask Placement_layerMask;

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 300, Placement_layerMask))
        {
            Lastposition = hit.point;
        }

        return Lastposition;

    }

}
