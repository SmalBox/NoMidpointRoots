using Auto.Logic.Core.Message;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : UIBase
{
    [SerializeField] private Text timeText;
    [SerializeField] private Text scoreText;
    
    [SerializeField] private Button returnBtn;

    public override void Init()
    {
        base.Init();
        timeText.text = TimeMgr.Ins.DefaultTime.ToString();
        scoreText.text = "0";
    }

    public override void Register()
    {
        base.Register();
        Messenger<int>.AddListener(MessengerEventType.INFO_TIME, OnChangeInfoTime);
        Messenger<int>.AddListener(MessengerEventType.INFO_SCORE, OnChangeInfoScore);
        returnBtn.onClick.AddListener(OnReturnBtn);
    }

    public override void UnRegister()
    {
        base.UnRegister();
        Messenger<int>.RemoveListener(MessengerEventType.INFO_TIME, OnChangeInfoTime);
        Messenger<int>.RemoveListener(MessengerEventType.INFO_SCORE, OnChangeInfoScore);
        returnBtn.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 时间变化
    /// </summary>
    /// <param name="time"></param>
    private void OnChangeInfoTime(int time)
    {
        timeText.text = time.ToString();
    }

    /// <summary>
    /// 分数变化
    /// </summary>
    /// <param name="score"></param>
    private void OnChangeInfoScore(int score)
    {
        scoreText.text = score.ToString();
    }

    private void OnReturnBtn()
    {
        GameMain.Ins.RestartGame();
        SoundMgr.Ins.PlaySound(SoundMgr.SoundType.Click);       
    }
}
