using UnityEngine.SceneManagement;   // 为了能重新加载场景
using UnityEngine;
using TMPro;

public class GameBehavior : MonoBehaviour
{
    // 单例：让别的脚本能通过 GameBehavior.Instance 找到这个总控台
    public static GameBehavior Instance;

    // 当前游戏状态。enum 的定义在 Utilities 静态类里
    public Utilities.GameState State;

    // 屏幕上显示分数的文字，从 Inspector 拖进来
    [SerializeField] private TMP_Text _scoreTextUI;

    // "Game Over" 文字，从 Inspector 拖进来
    [SerializeField] private TMP_Text _gameOverTextUI;

    // "Paused" 文字，从 Inspector 拖进来
    [SerializeField] private TMP_Text _pauseTextUI;

    // 播声音的组件，从 Inspector 拖进来
    [SerializeField] private AudioSource _audioSource;

    // 吃到苹果的音效，从 Inspector 拖进来
    [SerializeField] private AudioClip _eatClip;

    // 分数的后台变量，私有，外面不能直接乱改
    private int _score = 0;

    // Score 属性：外面通过它来读/改分数
    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            _scoreTextUI.text = "" + _score;
        }
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // 开局：状态设成 Play，时间恢复正常流动
        State = Utilities.GameState.Play;
        Time.timeScale = 1f;

        Score = 0;

        // 用 enabled 控制文字显不显示（不是 SetActive）
        _gameOverTextUI.enabled = false;
        _pauseTextUI.enabled = false;
    }

   void Update()
    {
        // 死了之后按 R 重开一局：重新加载当前场景
        if (State == Utilities.GameState.GameOver && Input.GetKeyUp(KeyCode.R))
        {
            Time.timeScale = 1f;   // 先把时间恢复，否则新场景也是冻住的
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        // 已经 GameOver 了就不允许再暂停
        if (State == Utilities.GameState.GameOver)
        {
            return;
        }

        // ... 下面按 P 暂停那段保持原样
    }

    // 游戏结束
    public void GameOver()
    {
        State = Utilities.GameState.GameOver;
        Time.timeScale = 0f;              // 冻住整个游戏
        _gameOverTextUI.enabled = true;
    }

    // 播放吃苹果的音效。别的脚本可以调用它
    public void PlayEatSound()
    {
        _audioSource.PlayOneShot(_eatClip);
    }
}