using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 根须管理类
/// </summary>
public class RootManager : MonoBehaviour
{
    [SerializeField] RootBranch branchPrefab = null;
    [SerializeField] Vector3 startPos = Vector3.zero;

    private List<RootBranch> AllBranchList = new List<RootBranch>();
    private RootBranch curBranch = null;

    private void Awake()
    {
        instance = this;
        
    }

    /// <summary>
    /// 创建新的根须
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="bornDir"></param>
    public void CreateNewBranch(Vector3 pos, Vector3 bornDir, bool needGrowth = true)
    {
        curBranch = Instantiate<RootBranch>(branchPrefab, transform);
        curBranch.transform.localPosition = Vector3.zero;
        curBranch.Born(pos, bornDir, 1, needGrowth);
    }

    /// <summary>
    /// 游戏开始
    /// </summary>
    public void StartGrowth()
    {
        CreateNewBranch(startPos, Vector3.down);
    }

    private static RootManager instance = null;
    public static RootManager Inst { get => instance; }
}
