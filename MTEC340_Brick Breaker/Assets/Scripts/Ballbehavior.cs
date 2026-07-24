using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [SerializeField] private float _speed = 7.0f;

    [SerializeField] private AudioClip _hitWallClip;
    [SerializeField] private AudioClip _hitPaddleClip;

    private int _xDirection;
    private int _yDirection;
    private Rigidbody2D _rb;
    private AudioSource _source;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _source = GetComponent<AudioSource>();

        _xDirection = Random.value < 0.5f ? -1 : 1;
        _yDirection = 1;
    }

    private void FixedUpdate()
    {
        bool isPaused = GameBehavior.Instance.State == GameBehavior.GameState.Paused;
        _rb.simulated = !isPaused;

        if (isPaused) return;

        Vector2 movement = new Vector2(
            _speed * _xDirection,
            _speed * _yDirection
        ) * Time.fixedDeltaTime;

        _rb.MovePosition(_rb.position + movement);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            _xDirection *= -1;
            PlaySound(_hitWallClip);
        }

        if (other.gameObject.CompareTag("Top"))
        {
            _yDirection *= -1;
            PlaySound(_hitWallClip);
        }

        if (other.gameObject.CompareTag("Paddle"))
        {
            _yDirection *= -1;
            PlaySound(_hitPaddleClip);
        }

        if (other.gameObject.CompareTag("Brick"))
        {
            _yDirection *= -1;
            // Brick 碰撞音效由 BrickBehavior 自己播放
        }
    }

    private void PlaySound(AudioClip clip)
    {
        _source.clip = clip;
        _source.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameBehavior.Instance.BallLost();
        Destroy(gameObject);
    }
}