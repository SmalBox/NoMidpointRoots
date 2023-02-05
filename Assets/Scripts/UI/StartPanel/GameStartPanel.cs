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
    
    public override void Init()
    {
        base.Init();
    }

    public override void Register()
    {
        base.Register();
        startBtn.onClick.AddListener(OnStartBtn);
        copyrightBtn.onClick.AddListener(OnCopyrightBtn);
    }

    public override void UnRegister()
    {
        base.UnRegister();
        startBtn.onClick.RemoveAllListeners();
        copyrightBtn.onClick.RemoveAllListeners();
    }

    private void OnStartBtn()
    {
        GameMain.Ins.StartGame();
        Destroy(gameObject);
    }

    private void OnCopyrightBtn()
    {
        copyrightContentBtn.gameObject.SetActive(true);
    }
}
