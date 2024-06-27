using UnityEngine;

public class FollowTargetPosition : MonoBehaviour
{
    public Transform target; // 要跟随的目标物体的 Transform
    public Vector3 velocity { get; private set; } // 公共的速度变量
    private Rigidbody rb; // player 的 Rigidbody 组件

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on player object.");
            return;
        }

        if (target == null)
        {
            Debug.LogError("Target transform not assigned.");
            return;
        }
    }

    void FixedUpdate()
    {
        // 获取目标物体的位置
        Vector3 targetPosition = target.position;

        // 计算 player 的速度，需要在两个 FixedUpdate 之间进行差分来获取速度
        velocity = (targetPosition - transform.position) / Time.fixedDeltaTime;

        // 设置 player 的位置与目标物体位置一致
        transform.position = targetPosition;
    }
}