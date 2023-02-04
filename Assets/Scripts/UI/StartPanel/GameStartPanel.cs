using UnityEngine;
using UnityEngine.UI;

public class GameStartPanel : UIBase
{
    /// <summary> 开始按钮 </summary>
    public Button startBtn;
    
    public override void Init()
    {
        base.Init();
    }

    public override void Register()
    {
        base.Register();
        startBtn.onClick.AddListener(OnStartBtn);
    }

    public override void UnRegister()
    {
        base.UnRegister();
        startBtn.onClick.RemoveAllListeners();
    }

    private void OnStartBtn()
    {
        GameMain.Ins.StartGame();
        Destroy(gameObject);
    }
}
