using System;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init()
    {
        Register();
    }

    /// <summary> 注册消息 </summary>
    public virtual void Register()
    {
    }

    /// <summary> 注销消息 </summary>
    public virtual void UnRegister()
    {
    }
    
    /// <summary>
    /// 统一刷新UI
    /// </summary>
    public virtual void RefreshUI()
    {
    }

    /// <summary>
    /// 关闭UI
    /// </summary>
    public virtual void CloseUI()
    {
        UnRegister();
    }

    private void Start()
    {
        Init();
        RefreshUI();
    }

    private void OnDestroy()
    {
        CloseUI();
    }
}
