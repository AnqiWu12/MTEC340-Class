using UnityEngine;
using TMPro;

public class GameBehavior : MonoBehaviour
{
    // Both instance and access point
    public static GameBehavior Instance;

    [SerializeField] private GameObject _ballPrefab;
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
        // Software Design Patterns
        // Singleton Pattern: Enforces that there is only ever one class
        // throughout the whole execution of the program
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

    private void ResetGame()
    {
        Score = 0;
        SpawnBall();
    }

    private void SpawnBall()
    {
        Instantiate(_ballPrefab);
    }
}