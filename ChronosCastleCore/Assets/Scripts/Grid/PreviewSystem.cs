using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField]
    private float previewYOffset = 0.06f;

    [SerializeField]
    private GameObject cellIndicator;
    private GameObject previewObject;

    [SerializeField]
    private Material previewMaterialPrefab;
    private Material previewMaterialInstance;

    private Renderer cellIndicatorRenderer;

    private void Start()
    {
        previewMaterialInstance = new Material(previewMaterialPrefab);
        cellIndicator.SetActive(false);
        cellIndicatorRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        //if (previewObject)
        //    Destroy(previewObject);
        previewObject = Instantiate(prefab);
        PreparePreview(previewObject);
        PrepareCursor(size);
        cellIndicator.SetActive(true);
    }

    private void PrepareCursor(Vector2 size)
    {
        if (size.x > 0 || size.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(size.x/2, 1, size.y/2);
            cellIndicatorRenderer.material.mainTextureScale = size;
        }
    }

    private void PreparePreview(GameObject previewObject)
    {
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterialInstance;
            }
            renderer.materials = materials;
        }
    }

    public void StopShowingPreview()
    {
        cellIndicator.SetActive(false);

        if (previewObject != null)
            Destroy(previewObject);
    }

    public void UpdatePos(Vector3 pos, bool valid)
    {
        if (previewObject != null)
        {
            MovePreview(pos);
            ApplyFeedbackToPreview(valid);
        }
        MoveCursor(pos);
        ApplyFeedbackToCursor(valid);
    }

    private void ApplyFeedbackToPreview(bool valid)
    {
        UnityEngine.Color c = valid ? UnityEngine.Color.white : UnityEngine.Color.red;
        c.a = 0.5f;
        previewMaterialInstance.color = c;

    }
    private void ApplyFeedbackToCursor(bool valid)
    {
        UnityEngine.Color c = valid ? UnityEngine.Color.white : UnityEngine.Color.red;
        c.a = 0.5f;

        cellIndicatorRenderer.material.color = c;
    }

    private void MoveCursor(Vector3 pos)
    {
        cellIndicator.transform.position = pos;
    }

    private void MovePreview(Vector3 pos)
    {
        previewObject.transform.position = new Vector3(pos.x, pos.y + previewYOffset, pos.z);
    }

    internal void StartShowingRemovePreview()
    {
        cellIndicator.SetActive(true);
        PrepareCursor(Vector2Int.one);
        ApplyFeedbackToCursor(false);
    }
}

