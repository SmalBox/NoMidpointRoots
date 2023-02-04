using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GameEndPanel : UIBase
{
    /// <summary> 得分文本 </summary>
    [SerializeField] private Text scoreText;
    /// <summary> 结束动画 </summary>
    [SerializeField] private Animator endAnimator;
    
    /// <summary> 开始按钮 </summary>
    public Button restartBtn;
    
    public override void Init()
    {
        base.Init();
    }

    public void SetData(int score)
    {
        scoreText.text = score.ToString();
        endAnimator.Play(GetEndAnimName(score));
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

    /// <summary>
    /// 根据分数播放结束动画
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    private string GetEndAnimName(int score)
    {
        string endAnimName = "PerishAnim";
        if (GameMain.Ins.scoreStageList.Count > 0 &&
            GameMain.Ins.scoreStageList.Count == GameMain.Ins.scoreStageAnimNameList.Count)
        {
            for (int i = 0; i < GameMain.Ins.scoreStageList.Count; i++)
            {
                if (score >= GameMain.Ins.scoreStageList[i])
                {
                    endAnimName = GameMain.Ins.scoreStageAnimNameList[i];
                }
            }
        }
        else
        {
            Debug.LogWarning(
                "<color=yellow>检查GameMain的 scoreStageList和scoreStageAnimNameList 配置</color>"
                );
        }

        return endAnimName;
    }
}
