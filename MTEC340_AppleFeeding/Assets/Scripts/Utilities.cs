using System;
using UnityEngine;

// 全局共享的小工具类。不挂在任何物体上，只用来放游戏状态这类到处要用的东西。
public static class Utilities
{
    // 游戏当前所处的状态
    // Play     = 正常游玩
    // Pause    = 暂停
    // GameOver = 倒计时结束、游戏结算
    public enum GameState
    {
        Play,
        Pause,
        GameOver
    }
}