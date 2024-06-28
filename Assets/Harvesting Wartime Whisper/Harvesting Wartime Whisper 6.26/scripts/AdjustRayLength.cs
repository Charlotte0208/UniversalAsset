using UnityEngine;
using Rokid.UXR.Interaction;
using System.Collections;

public class AdjustRayLength : MonoBehaviour
{
    public float NewMaxRayLength = 10.0f; // 你想要设置的新的 Max Ray Length 值

    void Start()
    {
        // 启动一个协程，在下一帧执行 AdjustMaxRayLength 方法
        StartCoroutine(AdjustMaxRayLengthAfterStart());
    }

    IEnumerator AdjustMaxRayLengthAfterStart()
    {
        // 等待一帧，以确保所有物体的 Start 方法都已经执行完毕
        yield return null;

        // 找到所有 tag 为 "RKInput" 的物体
        GameObject[] rkInputObjects = GameObject.FindGameObjectsWithTag("RKInput");

        foreach (GameObject rkInputObject in rkInputObjects)
        {
            // 在每个 tag 为 "RKInput" 的物体中查找所有子物体
            AdjustMaxRayLengthInChildren(rkInputObject.transform);
        }
    }

    void AdjustMaxRayLengthInChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // 如果找到了叫做 "ThreeDofRayInteractor" 的物体
            if (child.name == "ThreeDofRayInteractor")
            {
                // 尝试获取 Ray Interactor 脚本
                RayInteractor rayInteractor = child.GetComponent<RayInteractor>();
                if (rayInteractor != null)
                {
                    // 设置 Max Ray Length 的值
                    rayInteractor.MaxRayLength = NewMaxRayLength;
                    Debug.Log($"Updated Max Ray Length for {child.name} to {NewMaxRayLength}");
                }
            }

            // 递归查找子物体
            AdjustMaxRayLengthInChildren(child);
        }
    }
}