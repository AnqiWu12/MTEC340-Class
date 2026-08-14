using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

// 一只可以被喂的动物：在场地里乱逛，被苹果喂三次后长大、打嗝、消失并加分
// 支持动物模型自带的走路动画（靠设置 Animator 的参数来驱动腿部动画）
[RequireComponent(typeof(AudioSource))]
public class Animal : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 3.0f;          // 前进速度
    [SerializeField] private float _sphereRadius = 0.75f;  // 探路用的球体半径
    [SerializeField] private float _obstacleRange = 3.0f;  // 离障碍多近就转向

    [Header("Feeding")]
    [SerializeField] private int _feedsToFull = 3;         // 喂几口算饱
    [SerializeField] private float _growPerFeed = 0.3f;    // 每喂一口长大多少

    [Header("Audio")]
    [SerializeField] private AudioClip _feedClip;    // 没饱那口的音效
    [SerializeField] private AudioClip _fullClip;    // 喂饱消失的音效

    [Header("Animation")]
    [SerializeField] private string _walkParam = "Vert";   // 模型 Animator 里控制走路的参数名
    [SerializeField] private float _walkValue = 1.0f;      // 走路时把参数设成多少

    private AudioSource _audioSource;
    private Animator _animator;         // 从子物体上的模型拿到的 Animator

    private bool _isFull = false;       // 饱了之后就不再响应，避免重复加分

    // 被喂的次数用属性包起来，每次赋值顺便把动物放大一点
    private int _feedCount = 0;

    public int FeedCount
    {
        get => _feedCount;
        set
        {
            _feedCount = value;
            transform.localScale += Vector3.one * _growPerFeed;
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        // 走路动画的 Animator 在模型子物体上，往下找一层
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_isFull)
        {
            // 饱了就停下，动画也切回站立
            SetWalkAnim(0.0f);
            return;
        }

        // 一直往前走
        transform.Translate(0.0f, 0.0f, _speed * Time.deltaTime);

        // 在动，就播走路动画
        SetWalkAnim(_walkValue);

        // 用射线探前方，快撞墙时随机转个方向，看起来像在自由乱逛
        Ray ray = new(transform.position, transform.forward);

        if (Physics.SphereCast(ray, _sphereRadius, out RaycastHit hit, 100.0f))
        {
            if (hit.distance < _obstacleRange)
            {
                float theta = Random.Range(-135.0f, 135.0f);
                transform.Rotate(0.0f, theta, 0.0f);
            }
        }
    }

    // 设走路动画参数，没有 Animator 或参数名时不报错
    private void SetWalkAnim(float value)
    {
        if (_animator != null && !string.IsNullOrEmpty(_walkParam))
        {
            _animator.SetFloat(_walkParam, value);
        }
    }

    // 扔中一次苹果时调用（射线或飞行苹果都走这里）
    public void Feed()
    {
        if (_isFull) return;

        // 强化状态下一口顶三口，直接喂饱还保留长大的动画
        bool powered = GameBehavior.Instance != null && GameBehavior.Instance.IsPowered;
        int amount = powered ? 3 : 1;

        FeedCount += amount;

        if (FeedCount >= _feedsToFull)
        {
            BecomeFull();
        }
        else
        {
            // 还没饱，播喂食音效
            if (_feedClip != null)
            {
                _audioSource.PlayOneShot(_feedClip);
            }
        }
    }

    // 喂饱了：加分、播打嗝音效、消失
    private void BecomeFull()
    {
        _isFull = true;

        if (GameBehavior.Instance != null)
        {
            GameBehavior.Instance.AddScore();
        }

        // 马上要销毁了，用 PlayClipAtPoint 独立播放，声音不会被打断
        if (_fullClip != null)
        {
            AudioSource.PlayClipAtPoint(_fullClip, transform.position);
        }

        Destroy(gameObject);
    }

    // 消失时把自己从生成器的存活列表里移除，列表空了才会刷下一波
    private void OnDestroy()
    {
        EnemyWaves waveSystem = transform.GetComponentInParent<EnemyWaves>();

        if (waveSystem)
        {
            waveSystem.Enemies.Remove(gameObject);
        }
    }
}