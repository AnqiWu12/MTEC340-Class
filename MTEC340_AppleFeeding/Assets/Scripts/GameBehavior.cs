using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// 游戏的核心管理器，用单例让任何脚本都能直接找到它。
// 一处集中管理分数、倒计时、游戏状态、黄金苹果强化、最高分记录、以及 UI 和结算音效。
public class GameBehavior : MonoBehaviour
{
    // 全局唯一实例，其他脚本用 GameBehavior.Instance 访问
    public static GameBehavior Instance { get; private set; }

    // 存最高分用的键名，PlayerPrefs 靠它找到存的数据
    private const string HighScoreKey = "AppleFarm_HighScore";

    [Header("UI References")]
    [SerializeField] private TMP_Text _scoreText;      // 分数
    [SerializeField] private TMP_Text _timerText;      // 剩余时间
    [SerializeField] private TMP_Text _bestScoreText;  // 右上角常驻的最高分
    [SerializeField] private TMP_Text _gameOverText;   // 结算文字，平时藏起来
    [SerializeField] private TMP_Text _powerText;      // 强化提示，平时藏起来

    [Header("Audio")]
    [SerializeField] private AudioClip _gameEndClip;   // 时间到时的结算音效

    [Header("Timer")]
    [SerializeField] private float _startTime = 60.0f; // 一局多长
    private float _timeLeft;

    [Header("Power-up")]
    [SerializeField] private float _powerDuration = 5.0f;  // 吃到黄金苹果后强化几秒

    [Header("Scene")]
    [SerializeField] private string _menuSceneName = "MainMenu";  // 主菜单场景名，按 Esc 回这里

    // 是否处于强化状态，动物喂食时会读这个来决定一口喂多少
    public bool IsPowered { get; private set; } = false;

    private Utilities.GameState _state = Utilities.GameState.Play;

    // 分数用属性包起来，赋值时顺便刷新 UI，外部只管加分不用管显示
    private int _score = 0;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            if (_scoreText != null)
            {
                _scoreText.text = "Fed: " + _score;
            }
        }
    }

    private void Awake()
    {
        // 场景里只允许有一个管理器，多出来的销毁掉
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        // 每局开始的初始化：满时间、清零分数、正常状态、时间正常流动
        _timeLeft = _startTime;
        Score = 0;
        _state = Utilities.GameState.Play;
        Time.timeScale = 1.0f;

        // 开局把右上角的最高分显示出来
        UpdateBestScoreUI();

        // 结算和强化提示先藏起来
        if (_gameOverText != null)
        {
            _gameOverText.enabled = false;
        }
        if (_powerText != null)
        {
            _powerText.enabled = false;
        }
    }

    private void Update()
    {
        // 按 Esc 随时回主菜单
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenu();
            return;
        }

        // 按 P 暂停/继续，结束后就不能再暂停了
        if (_state != Utilities.GameState.GameOver && Input.GetKeyUp(KeyCode.P))
        {
            _state = _state == Utilities.GameState.Play
                ? Utilities.GameState.Pause
                : Utilities.GameState.Play;

            // 暂停就把时间冻住，继续就恢复
            Time.timeScale = _state == Utilities.GameState.Pause ? 0.0f : 1.0f;
        }

        // 只有正常游玩时才走倒计时
        if (_state == Utilities.GameState.Play)
        {
            // 用 deltaTime 递减，保证不同帧率下时间一致
            _timeLeft -= Time.deltaTime;

            if (_timeLeft <= 0.0f)
            {
                _timeLeft = 0.0f;
                EndGame();
            }

            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        if (_timerText != null)
        {
            // 向上取整，剩 0.3 秒也显示 1，真正归零才显示 0
            int secondsLeft = Mathf.CeilToInt(_timeLeft);
            _timerText.text = "Time: " + secondsLeft;
        }
    }

    // 把右上角的最高分刷新成存档里的值
    private void UpdateBestScoreUI()
    {
        if (_bestScoreText != null)
        {
            int highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
            _bestScoreText.text = "Best: " + highScore;
        }
    }

    // 喂饱一只动物就加一分，动物脚本会调这个
    public void AddScore()
    {
        Score += 1;
    }

    // 吃到黄金苹果时调用，开启强化并重新计时
    public void ActivatePower()
    {
        // 先停掉可能还在跑的旧计时，再重新开一个，这样连吃两个能刷新时长
        StopCoroutine(nameof(PowerCountdown));
        StartCoroutine(nameof(PowerCountdown));
    }

    private IEnumerator PowerCountdown()
    {
        IsPowered = true;

        if (_powerText != null)
        {
            _powerText.enabled = true;
            _powerText.text = "POWER!";
        }

        yield return new WaitForSeconds(_powerDuration);

        IsPowered = false;

        if (_powerText != null)
        {
            _powerText.enabled = false;
        }
    }

    // 时间到：切到结算状态，播结算音效，冻结画面，比对并记录最高分，显示结果
    private void EndGame()
    {
        _state = Utilities.GameState.GameOver;

        // 在冻结时间之前播结算音效
        if (_gameEndClip != null)
        {
            AudioSource.PlayClipAtPoint(_gameEndClip, Camera.main.transform.position);
        }

        Time.timeScale = 0.0f;

        // 读出存档里的历史最高分（第一次玩默认 0）
        int highScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        // 这局是否破纪录
        bool isNewBest = _score > highScore;
        if (isNewBest)
        {
            highScore = _score;
            PlayerPrefs.SetInt(HighScoreKey, highScore);  // 存回硬盘，下次打开还记得
            PlayerPrefs.Save();
            UpdateBestScoreUI();  // 破纪录了，右上角也刷新一下
        }

        if (_gameOverText != null)
        {
            _gameOverText.enabled = true;

            // 结算文字：这局分数 + 评价 + 历史最高分（破纪录就加一句）
            string result = "Time's up!\nYou fed " + _score + " animals!\n" + GetRating();
            result += "\nBest: " + highScore;
            if (isNewBest)
            {
                result += "\nNew Best!";
            }
            _gameOverText.text = result;
        }
    }

    // 根据分数给一句评价，纯粹为了好玩
    private string GetRating()
    {
        if (_score >= 10) return "Apple Master!";
        if (_score >= 5)  return "Nice!";
        return "Keep practicing!";
    }

    // 回主菜单：恢复时间流动再切场景，免得菜单被冻住
    private void ReturnToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(_menuSceneName);
    }
}