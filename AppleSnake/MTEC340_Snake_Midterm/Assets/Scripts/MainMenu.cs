using System.Collections;              // 为了能用 IEnumerator（协程）
using UnityEngine;
using UnityEngine.SceneManagement;     // 为了能切换场景

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    // 等多久再切场景（秒）。按你音效的长度调，在 Inspector 里可以改
    [SerializeField] private float _delay = 0.6f;

    // 安全阀：防止玩家连点按钮，同时启动好几个协程
    private bool _isBusy = false;

    void Start()
    {
        // 保险起见把时间恢复正常。因为 timeScale 会跨场景保留，
        // 如果它还停在 0，下面的 WaitForSeconds 会永远等不完
        Time.timeScale = 1f;
    }

    // 点 StartEat 按钮（音效由按钮的 OnClick 播，这里只负责等它播完再切场景）
    public void StartGame()
    {
        if (_isBusy) return;
        _isBusy = true;
        StartCoroutine(WaitThenLoad());
    }

    private IEnumerator WaitThenLoad()
    {
        // 等音效播一会儿，再切场景，这样声音不会被切断
        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene("GameScene");
    }

    // 点 I'm Full 按钮
    public void QuitGame()
    {
        if (_isBusy) return;
        _isBusy = true;
        StartCoroutine(WaitThenQuit());
    }

    private IEnumerator WaitThenQuit()
    {
        yield return new WaitForSeconds(_delay);

        #if UNITY_EDITOR
            // 在 Unity 编辑器里运行时，就停止播放模式
            EditorApplication.isPlaying = false;
        #else
            // 打包成 App 后运行时，才是真正退出
            Application.Quit();
        #endif
    }
}