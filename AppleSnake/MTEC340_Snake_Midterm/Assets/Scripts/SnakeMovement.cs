using System.Collections.Generic;   // 为了能用 List（列表）这个功能
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    // _moveInterval = 每隔多少秒走一格。数字越小，蛇越快
    [SerializeField] private float _moveInterval = 0.15f;

    // 身体节的模板（预制体），从 Inspector 拖进来
    [SerializeField] private Transform _bodyPrefab;

    // 苹果物体，从 Inspector 拖进来。蛇靠它的位置判断有没有吃到
    [SerializeField] private Transform _apple;

    // _direction = 蛇当前朝哪个方向走。一开始设成"往右"
    private Vector2 _direction = Vector2.right;

    // _moveTimer = 一个计时器，用来攒时间
    private float _moveTimer = 0f;

    // 用一个"列表"按顺序存蛇的每一节，第 0 个是头，后面依次是身体
    private List<Transform> _segments = new List<Transform>();

    // 一个开关，标记"下一步要不要变长"
    private bool _grow = false;

    // 标记游戏是否已经开始动。玩家按第一个方向键前，蛇是静止的
    private bool _hasStarted = false;

    void Start()
    {
        // 游戏一开始，蛇头自己就是第一节，先把它放进列表
        _segments.Add(transform);

        // 开局把苹果随机放到一个空格子上
        MoveApple();
    }

    void Update()
    {
        // 只有 Play 状态才处理输入（Pause 和 GameOver 都不动）
        if (GameBehavior.Instance.State != Utilities.GameState.Play)
        {
            return;
        }

        // 每一帧都读一下键盘，看玩家有没有要改方向
        HandleInput();

        // 玩家还没按过任何方向键，蛇就先静止不动，等你准备好
        if (!_hasStarted)
        {
            return;
        }

        // 每帧把这一帧过去的时间加进计时器
        // 用 Time.deltaTime，保证不管电脑快慢，蛇速都一样
        _moveTimer += Time.deltaTime;

        // 当攒够的时间 >= 一个间隔，就走一格，然后把计时器清零重新攒
        if (_moveTimer >= _moveInterval)
        {
            _moveTimer = 0f;
            Move();
        }
    }

    // 移动的方法：让整条蛇往前走一格
    private void Move()
    {
        // 先记住尾巴现在的位置（万一这一步要变长，就在这儿放新的一节）
        Vector2 tailPosition = _segments[_segments.Count - 1].position;

        // 算出蛇头"下一格"要去哪
        Vector2 nextPosition = (Vector2)_segments[0].position + _direction;

        // 死亡判定：撞到自己——下一格是否已经被某节身体占着
        // （从第 1 节开始比，跳过第 0 节也就是头自己）
        for (int i = 1; i < _segments.Count; i++)
        {
            if ((Vector2)_segments[i].position == nextPosition)
            {
                GameBehavior.Instance.GameOver();
                return;   // 死了，别再往前走
            }
        }

        // 在移动之前先判断：下一格是不是苹果所在的格子？
        bool ateApple = (nextPosition == (Vector2)_apple.position);

        // 身体跟随：从最后一节往前，每节都搬到"前面一节"刚才站的位置
        // 必须从后往前搬，否则前面的先动了，后面就会搬到错的地方
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        // 头往前迈一格
        _segments[0].position = nextPosition;

        // 如果吃到了苹果：这一步要变长，把苹果挪到新位置，并且加 1 分
        if (ateApple)
        {
            _grow = true;
            MoveApple();
            GameBehavior.Instance.Score++;   // 加一分
        }

        // 如果这一步要变长，就照着模板在"尾巴刚才的位置"复制一节新身体
        if (_grow)
        {
            Transform newSegment = Instantiate(_bodyPrefab, tailPosition, Quaternion.identity);
            _segments.Add(newSegment);
            _grow = false;   // 长完了，把开关关掉，等下次再变长
        }

        // 如果吃到了苹果：这一步要变长，把苹果挪到新位置，并且加 1 分
        if (ateApple)
        {
            _grow = true;
            MoveApple();
            GameBehavior.Instance.Score++;   // 加一分
            GameBehavior.Instance.PlayEatSound();   // 新增这一行：播吃的音效
        }
    }

    // 读键盘的方法：按了方向键，就尝试把 _direction 换成对应方向
    // 注意：这里只"改方向"，并不负责移动。移动交给上面的计时器
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TryChangeDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TryChangeDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TryChangeDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TryChangeDirection(Vector2.right);
        }
    }

    // 尝试改变方向，如果新方向正好和当前方向相反（会掉头撞自己），就忽略
    private void TryChangeDirection(Vector2 newDirection)
    {
        if (newDirection + _direction == Vector2.zero)
        {
            return;
        }

        _direction = newDirection;
        _hasStarted = true;   // 玩家按了方向键，游戏正式开始动
    }

    // 把苹果随机放到一个空格子上（不能放在蛇身上）
    private void MoveApple()
    {
        Vector2 newPos;
        bool onSnake;

        do
        {
            // Random.Range(-6, 7) 会随机给 -6 到 6 之间的整数（7 是取不到的上限）
            int x = Random.Range(-6, 7);
            int y = Random.Range(-6, 7);
            newPos = new Vector2(x, y);

            // 检查这个格子是不是正好压在蛇身上，是的话就重新随机一个
            onSnake = false;
            foreach (Transform seg in _segments)
            {
                if ((Vector2)seg.position == newPos)
                {
                    onSnake = true;
                    break;
                }
            }
        } while (onSnake);

        // 找到一个空格子了，把苹果放过去
        _apple.position = newPos;
    }
}