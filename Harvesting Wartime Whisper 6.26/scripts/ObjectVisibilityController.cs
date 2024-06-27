using UnityEngine;

public class ObjectVisibilityController : MonoBehaviour
{
    // 要控制的公共物体
    public GameObject targetObject;

    // 隐藏目标物体
    public void HideObject()
    {
        if (targetObject != null)
        {
            Debug.Log("111");
            targetObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Target object is not assigned.");
        }
    }

    // 显现目标物体
    public void ShowObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Target object is not assigned.");
        }
    }
}