using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            GameBehavior.Instance.Score++;
            Destroy(gameObject);
        }
    }
}