using UnityEngine;

public class InteractiveObjectController : MonoBehaviour
{
    public Canvas canvas;  // 需要显示和隐藏的Canvas
    private float distanceFromCamera = 1.1f;  // Canvas距离摄像机的距离
    public Vector3 canvasSize = new Vector3(0.001f, 0.001f, 0.001f);  // Canvas的大小

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        canvas.gameObject.SetActive(false);  // 初始时隐藏Canvas
        canvas.transform.localScale = canvasSize;  // 设置Canvas的大小
    }

    void Update()
    {
        CheckCanvasVisibility();
    }

    public void ShowCanvas()
    {
        canvas.gameObject.SetActive(true);
        UpdateCanvasPositionAndRotation();
    }

    public void HideCanvas()
    {
        canvas.gameObject.SetActive(false);
    }

    private void UpdateCanvasPositionAndRotation()
    {
        if (mainCamera != null)
        {
            // 设置Canvas的位置
            Vector3 canvasPosition = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;
            canvas.transform.position = canvasPosition;

            // 设置Canvas的朝向
            canvas.transform.LookAt(mainCamera.transform);
            canvas.transform.Rotate(0, 180, 0);  // 使Canvas正面朝向摄像机
        }
    }

    private void CheckCanvasVisibility()
    {
        Vector3 viewPos = mainCamera.WorldToViewportPoint(canvas.transform.position);
        if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1 || viewPos.z < 0)
        {
            canvas.gameObject.SetActive(false);  // 当Canvas不在摄像机视野内时隐藏
        }
    }
}