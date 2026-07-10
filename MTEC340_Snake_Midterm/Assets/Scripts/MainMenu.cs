using UnityEngine;
using UnityEngine.SceneManagement;   // 为了能切换场景

public class MainMenu : MonoBehaviour
{
    // 点开始按钮：切换到游戏场景
    public void StartGame()
    {
        // 引号里必须和你游戏场景的文件名完全一致
        SceneManager.LoadScene("GameScene");
    }

    // 点退出按钮：退出游戏
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");   // 在编辑器里点退出不会真关，这行让你在 Console 看到它确实被调用了
    }
}