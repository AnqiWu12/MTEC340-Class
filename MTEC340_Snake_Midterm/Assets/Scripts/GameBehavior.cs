using UnityEngine;
using TMPro;   // 为了能用 TextMeshPro 文字

public class GameBehavior : MonoBehaviour
{
    // enum：给"游戏状态"这组固定选项起名字，一共三种
    public enum GameState
    {
        Playing,    // 进行中
        Paused,     // 暂停
        GameOver    // 结束
    }

    // 单例：让别的脚本能通过 GameBehavior.Instance 找到这个总控台
    public static GameBehavior Instance;

    // 当前游戏处于哪个状态，一开始是"进行中"
    public GameState State = GameState.Playing;

    // 屏幕上显示分数的那个文字，从 Inspector 拖进来
    [SerializeField] private TMP_Text _scoreTextUI;

    // 结束时显示 "Game Over" 的文字，从 Inspector 拖进来
    [SerializeField] private TMP_Text _gameOverTextUI;

    // 分数的"后台变量"，私有，外面不能直接乱改
    private int _score = 0;

    // Score 属性：外面通过它来读/改分数
    public int Score