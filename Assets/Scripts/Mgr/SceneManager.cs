using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景管理
/// </summary>
public class SceneManager : MonoBehaviour
{
    private static SceneManager instance = null;
    public static SceneManager Inst {get => instance; }

    [SerializeField] GameObject hideObj = null;
    [SerializeField] List<SceneObjectBase> allAssetPrefabs = null;
    [SerializeField] Transform assetRoot = null;

    private List<SceneObjectBase> curAssets = new List<SceneObjectBase>();

    private void Awake() 
    {
        instance = this;
    }

    public void OnGameStart()
    {
        hideObj.SetActive(true);
        //创建资源
        CreatAssets();
        //树根生长
        RootManager.Inst.StartGrowth();
    }

    /// <summary>
    /// 创建资源
    /// </summary>
    private void CreatAssets()
    {
        float x = -8;
        foreach (var item in allAssetPrefabs)
        {
            var asset = Instantiate<SceneObjectBase>(item, assetRoot);
            asset.transform.position = new Vector3(x, 0, 0);
            x+=3;
        }
    }
}
