using UnityEngine;
using TMPro;

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;
    public enum GameState
    {
        Playing,
        Paused
    }

    public GameState State { get; private set; } = GameState.Playing;

    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _ballSpawnPoint;
    [SerializeField] private TMP_Text _scoreTextUI;

    private int _score;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            _scoreTextUI.text = Score.ToString();
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        State = State == GameState.Playing ? GameState.Paused : GameState.Playing;
    }

    private void ResetGame()
    {
        Score = 0;
        SpawnBall();
    }

    private void SpawnBall()
    {
        Instantiate(_ballPrefab, _ballSpawnPoint.position, Quaternion.identity);
    }

    public void BallLost()
    {
        SpawnBall();
    }
}