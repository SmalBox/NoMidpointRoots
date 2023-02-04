using System.Collections;
using Auto.Core;
using Auto.Logic.Core.Message;
using UnityEngine;

public class TimeMgr : SingletonMonoClass<TimeMgr>
{
    /// <summary> 默认时间 </summary>
    private int defaultTime;

    /// <summary> 当前时间 </summary>
    private int curTime;

    /// <summary> 倒计时协程 </summary>
    private Coroutine countdownCoroutine;
    
    public int DefaultTime
    {
        get { return defaultTime; }
    }

    public int CurTime
    {
        get { return curTime; }
    }
    
    public void Init(int initTime)
    {
        defaultTime = initTime;
    }

    /// <summary> 启动倒计时 </summary>
    public void StartCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        curTime = DefaultTime;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        Messenger<int>.Broadcast(MessengerEventType.INFO_TIME, curTime--,
                MessengerMode.DONT_REQUIRE_LISTENER);
        while (curTime >= 0)
        {
            yield return new WaitForSeconds(1);
            Messenger<int>.Broadcast(MessengerEventType.INFO_TIME, curTime--,
                MessengerMode.DONT_REQUIRE_LISTENER);
        }
    }
}
