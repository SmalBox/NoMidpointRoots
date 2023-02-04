using System;
using System.Collections;
using System.Collections.Generic;
using Auto.Logic.Core.Message;
using UnityEngine;

public class GameMain : SingletonMonoClass<GameMain>
{
    #region 面板配置

    [Header("游戏配置：")]
    public int gameTime = 60;

    [Header("End动画，分数阶段：(与下一项的个数顺序要对应！)")]
    public List<int> scoreStageList;
    [Header("End动画，分数阶段对应动画名字：")]
    public List<string> scoreStageAnimNameList;

    #endregion
    private void Start()
    {
        Init();
    }

    private void OnDestroy()
    {
        UnRegister();
    }

    private void Init()
    {
        TimeMgr.Ins.Init(gameTime);
        Register();
    }

    private void Register()
    {
        Messenger<int>.AddListener(MessengerEventType.PROGRESS_END, OnEndGame);
    }
    private void UnRegister()
    {
        Messenger<int>.RemoveListener(MessengerEventType.PROGRESS_END, OnEndGame);
    }

    private void OnEndGame(int score)
    {
        // 生成结束面板
        GameEndPanel gameEndPanel = (Instantiate(
            Resources.Load("Prefabs/UI/GameEndPanel"),
            GameObject.Find("UICanvas").transform) as GameObject)?.GetComponent<GameEndPanel>();
        if (gameEndPanel != null)
            gameEndPanel.SetData(score);
    }

    /// <summary> 开始游戏 </summary>
    public void StartGame()
    {
        Debug.Log("开始游戏");
        // 生成信息面板
        InfoPanel infoPanel = (Instantiate(
            Resources.Load("Prefabs/UI/InfoPanel"),
            GameObject.Find("UICanvas").transform) as GameObject)?.GetComponent<InfoPanel>();
        // 启动倒计时
        TimeMgr.Ins.StartCountdown();
    }

    public void RestartGame()
    {
        Debug.Log("重新开始游戏");
        // 简单做，直接重新加载场景
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void Update()
    {
        // 测试加分数
        if (Input.GetMouseButtonDown(0))
        {
            Messenger<int>.Broadcast(MessengerEventType.DATA_CHANGE_SCORE, 1);
        }
    }
}
