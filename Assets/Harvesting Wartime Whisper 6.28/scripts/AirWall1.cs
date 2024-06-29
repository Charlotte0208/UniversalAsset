using UnityEngine;

public class AirWall1 : MonoBehaviour
{
    private GameObject player;
    private FollowTargetPosition followScript; // 引用 FollowTargetPosition 脚本
    private GameObject world;

    void Start()
    {
        // 查找带有 "Player" 标签的游戏对象
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object with tag 'Player' not found.");
            return;
        }

        // 获取 FollowTargetPosition 脚本
        followScript = player.GetComponent<FollowTargetPosition>();
        if (followScript == null)
        {
            Debug.LogError("Player object does not have the FollowTargetPosition script.");
            return;
        }

        // 使用传入的 world 对象引用
        world = GameObject.FindGameObjectWithTag("world");
        if (world == null)
        {
            Debug.LogError("World object with tag 'world' not found.");
            return;
        }
    }

    void LateUpdate()
    {
        // 刷新携带此脚本的物体的世界 y 坐标到 0
        Vector3 position = transform.position;
        position = new Vector3(position.x, 0, position.z);
        transform.position = position;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            // 获取玩家的速度向量
            Vector3 playerVelocity = followScript.velocity;

            // 计算空气墙需要远离玩家的位移
            Vector3 moveDirection = playerVelocity.normalized;
            float moveDistance = playerVelocity.magnitude * Time.deltaTime;
            moveDistance *= 1.1f;

            // 将 world 远离玩家
            world.transform.position += moveDirection * moveDistance;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            // 当玩家离开空气墙时，可以在这里处理一些事情，如果需要的话
        }
    }
}