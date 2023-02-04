using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : SingletonMonoClass<GameMain>
{
    #region 面板配置

    [Header("游戏配置：")]
    public int gameTime = 60;
    

    #endregion
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        TimeMgr.Ins.Init(gameTime);
    }

    /// <summary> 开始游戏 </summary>
    public void StartGame()
    {
        Debug.Log("开始游戏");
        // 启动倒计时
        TimeMgr.Ins.StartCountdown();
    }
}
