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
        //CreatAssets();
        CreateAssets(30);
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

    private void CreateAssets(int num)
    {
        Vector3 randomPos = new Vector3();
        Vector3 randomRotate = new Vector3();
        for (int i = 0; i < num; i++)
        {
            // 随机选生成一个资源
            var item = allAssetPrefabs[Random.Range(0, allAssetPrefabs.Count)];
            var asset = Instantiate<SceneObjectBase>(item, assetRoot);
            // 随机位置生成资源
            asset.transform.localPosition = GetRandomPos(randomPos);
            randomRotate.z = Random.Range(0f, 360f);
            if (asset.ObjectType == ObjectType.Worms) asset.transform.Rotate(randomRotate);
        }
    }

    private List<Vector3> posList = new List<Vector3>();
    private Vector3 GetRandomPos(Vector3 randomPos)
    {
        do
        {
            randomPos.x = Random.Range(-8f, 8f);
            randomPos.y = Random.Range(0f, -7f);       
        } while (!CheckPosRight(randomPos));
        posList.Add(new Vector3(randomPos.x, randomPos.y));
        return randomPos;
    }

    private bool CheckPosRight(Vector3 randomPos)
    {
        if ((-2 > randomPos.x - 1) &&
            (2 < randomPos.x + 1) &&
            (0 > randomPos.y - 1) &&
            (-3 < randomPos.y + 1))
            return false;
        foreach (Vector3 pos in posList)
        {
            if ((pos.x > randomPos.x - 1 && pos.x < randomPos.x + 1) &&
                (pos.y > randomPos.y - 1 && pos.y < randomPos.y + 1))
                return false;
        }

        return true;
    }
}
