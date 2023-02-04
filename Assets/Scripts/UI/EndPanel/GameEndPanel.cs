using UnityEngine;
using UnityEngine.UI;

public class GameEndPanel : UIBase
{
    /// <summary> 得分文本 </summary>
    [SerializeField] private Text scoreText;
    
    /// <summary> 开始按钮 </summary>
    public Button restartBtn;
    
    public override void Init()
    {
        base.Init();
    }

    public void SetData(int score)
    {
        scoreText.text = score.ToString();
    }

    public override void Register()
    {
        base.Register();
        restartBtn.onClick.AddListener(OnRestartBtn);
    }

    public override void UnRegister()
    {
        base.UnRegister();
        restartBtn.onClick.RemoveAllListeners();
    }

    private void OnRestartBtn()
    {
        GameMain.Ins.RestartGame();
        Destroy(gameObject);
    }
}
