using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// 主菜单：给 Start / Quit 按钮提供点击方法。
// 点击音效先播一小会儿再切场景，免得切场景时把声音掐掉。
public class MainMenu : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private string _gameSceneName = "Game";   // 游戏场景名

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;   // 播按钮音效
    [SerializeField] private AudioClip _clickClip;       // 按钮点击音效
    [SerializeField] private float _clickDelay = 0.4f;   // 等音效多久再切场景

    private void Start()
    {
        // 进主菜单要能用鼠标点按钮，所以把游戏里锁住/隐藏的鼠标解开
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        PlayClick();

        // 用 Realtime 等，避免受 timeScale 影响
        yield return new WaitForSecondsRealtime(_clickDelay);

        // 保险起见恢复时间流动，防止上一局结束时 timeScale 还是 0
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(_gameSceneName);
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameRoutine());
    }

    private IEnumerator QuitGameRoutine()
    {
        PlayClick();

        yield return new WaitForSecondsRealtime(_clickDelay);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void PlayClick()
    {
        if (_audioSource != null && _clickClip != null)
        {
            _audioSource.PlayOneShot(_clickClip);
        }
    }
}