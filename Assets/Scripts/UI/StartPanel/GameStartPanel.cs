using UnityEngine;
using UnityEngine.UI;

public class GameStartPanel : UIBase
{
    /// <summary> 开始按钮 </summary>
    public Button startBtn;

    /// <summary> 版权按钮 </summary>
    public Button copyrightBtn;

    /// <summary> 版权内容 </summary>
    public Button copyrightContentBtn;

    /// <summary> 规则按钮 </summary>
    public Button ruleBtn;
    
     /// <summary> 规则内容 </summary>
    public Button ruleContentBtn;   
    
    /// <summary> 退出按钮 </summary>
    public Button exitBtn;
    
    public override void Init()
    {
        base.Init();
    }

    public override void Register()
    {
        base.Register();
        startBtn.onClick.AddListener(OnStartBtn);
        copyrightBtn.onClick.AddListener(OnCopyrightBtn);
        ruleBtn.onClick.AddListener(OnRuleBtn);
        exitBtn.onClick.AddListener(OnExitBtn);
    }

    public override void UnRegister()
    {
        base.UnRegister();
        startBtn.onClick.RemoveAllListeners();
        copyrightBtn.onClick.RemoveAllListeners();
        ruleBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.RemoveAllListeners();
    }

    private void OnStartBtn()
    {
        GameMain.Ins.StartGame();
        SoundMgr.Ins.PlaySound(SoundMgr.SoundType.Click);
        Destroy(gameObject);
    }

    private void OnCopyrightBtn()
    {
        copyrightContentBtn.gameObject.SetActive(true);
        SoundMgr.Ins.PlaySound(SoundMgr.SoundType.Click);
    }

    private void OnRuleBtn()
    {
        ruleContentBtn.gameObject.SetActive(true);
        SoundMgr.Ins.PlaySound(SoundMgr.SoundType.Click);
    }
    
    private void OnExitBtn()
    {
        SoundMgr.Ins.PlaySound(SoundMgr.SoundType.Click);
        Application.Quit();
    }
}
