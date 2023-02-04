using System;
using System.Collections;
using Auto.Core;
using Auto.Logic.Core.Message;
using UnityEngine;

public class ScoreMgr : SingletonMonoClass<ScoreMgr>
{
    /// <summary> 当前分数 </summary>
    private int curScore;
    
    public int CurScore
    {
        get { return curScore; }
    }

    private void Start()
    {
        Messenger<int>.AddListener(MessengerEventType.DATA_CHANGE_SCORE, OnChangeScore);
    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(MessengerEventType.DATA_CHANGE_SCORE, OnChangeScore);
    }

    private void OnChangeScore(int changeNum)
    {
        curScore += changeNum;
        if (curScore < 0) curScore = 0;
        Messenger<int>.Broadcast(MessengerEventType.INFO_SCORE, CurScore,
            MessengerMode.DONT_REQUIRE_LISTENER);
    }
}
