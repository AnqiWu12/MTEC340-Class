using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    [SerializeField] private AudioClip _hitBrickClip;
    [SerializeField] private int _lives = 3;

    [SerializeField] private Color[] _colors = new Color[]
    {
        new Color(1.0f, 0.75f, 0.8f),   // 3 血：浅粉色
        new Color(1.0f, 0.41f, 0.71f),  // 2 血：粉红色
        Color.red                        // 1 血：红色
    };

    private SpriteRenderer _spriteRenderer;

    private int Lives
    {
        get => _lives;
        set
        {
            _lives = value;
            UpdateColor();
        }
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    private void UpdateColor()
    {
        int index = Mathf.Clamp(_lives - 1, 0, _colors.Length - 1);
        _spriteRenderer.color = _colors[index];
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            AudioSource.PlayClipAtPoint(_hitBrickClip, transform.position);

            Lives--;

            if (Lives <= 0)
            {
                GameBehavior.Instance.Score++;
                Destroy(gameObject);
            }
        }
    }
}