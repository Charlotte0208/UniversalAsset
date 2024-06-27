using UnityEngine;

public class DistanceBasedVolume : MonoBehaviour
{
    public Transform player;         // 玩家对象
    public AudioSource audioSource;  // 音源组件
    public float maxDistance = 10f;  // 最大距离，超过这个距离音量为0
    public float maxVolume = 1f;     // 最大音量

    void Update()
    {
        // 计算玩家与音源的距离
        float distance = Vector3.Distance(player.position, transform.position);

        // 根据距离调整音量
        if (distance <= maxDistance)
        {
            // 距离越近，音量越大，距离为0时音量为maxVolume，距离为maxDistance时音量为0
            audioSource.volume = maxVolume * (1 - (distance / maxDistance));
        }
        else
        {
            // 超过最大距离时音量为0
            audioSource.volume = 0;
        }
    }
}