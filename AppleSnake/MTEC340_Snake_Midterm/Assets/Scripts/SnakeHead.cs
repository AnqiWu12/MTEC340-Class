using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    // 当蛇头碰到别的碰撞器时，Unity 会自动调用这个方法
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 如果碰到的东西贴着 "Wall" 标签，就判定游戏结束
        if (other.CompareTag("Wall"))
        {
            GameBehavior.Instance.GameOver();
        }
    }
}