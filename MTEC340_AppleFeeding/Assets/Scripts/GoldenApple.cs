using UnityEngine;

// 黄金苹果道具：玩家碰到它就进入强化状态，然后它自己消失。
// 需要一个勾了 Is Trigger 的 Collider 才能被走过触发。
[RequireComponent(typeof(Collider))]
public class GoldenApple : MonoBehaviour
{
    [Header("Pickup")]
    [SerializeField] private float _spinSpeed = 90.0f;   // 原地自转，让它更显眼
    [SerializeField] private AudioClip _pickupClip;      // 捡起来的音效

    private void Update()
    {
        transform.Rotate(0.0f, _spinSpeed * Time.deltaTime, 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 只有玩家能捡
        if (other.CompareTag("Player"))
        {
            if (GameBehavior.Instance != null)
            {
                GameBehavior.Instance.ActivatePower();
            }

            // 马上要销毁，独立播放音效免得被打断
            if (_pickupClip != null)
            {
                AudioSource.PlayClipAtPoint(_pickupClip, transform.position);
            }

            Destroy(gameObject);
        }
    }
}