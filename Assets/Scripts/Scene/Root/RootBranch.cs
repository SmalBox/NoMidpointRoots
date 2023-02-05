using System.Collections;
using System.Collections.Generic;
using Auto.Logic.Core.Message;
using UnityEngine;

public enum BranchStatu
{
    None,
    Birth,
    Normal,
    CanCreateOther,
    Static
}

/// <summary>
/// 树根
/// </summary>
public class RootBranch : MonoBehaviour
{
    [SerializeField] LineRenderer lineRender = null;
    [SerializeField] GameObject trigger = null;
    [SerializeField] float growthRate = 0.2f;//生长速度
    [SerializeField] float growthScale = 0.2f;//生长大小
    [SerializeField] float stopDistance = 0.1f;//停止距离

    private List<Vector3> brithList = new List<Vector3>();
    private Camera mainCamera = null;
    private bool isGrowthing = false;
    private float growthTimer = 0f;
    private Vector3 lastPos = Vector3.zero;
    private Vector3 growthDir = Vector3.zero;
    private Vector3 curTgtPos = Vector3.zero;

    private BranchStatu curStatu = BranchStatu.None;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable() 
    {
        Messenger<SceneObjectBase, RootBranch>.AddListener(MessengerEventType.ENTER_OBJECT, OnOtherEnter);
    }

    private void OnDisable() 
    {
        Messenger<SceneObjectBase, RootBranch>.RemoveListener(MessengerEventType.ENTER_OBJECT, OnOtherEnter);
    }

    private void Update() 
    {
        UpdateOperate();
        CheckCreateOther();
    }

    private void FixedUpdate() 
    {
        UpdateGrowth();
        UpdateColliderPos();
    }

    private void CalcultateGrowthDir(Vector3 tgtPos)
    {
        tgtPos.z = 0f;
        curTgtPos = tgtPos;
        growthDir = (curTgtPos - lastPos).normalized;
    }
    
    /// <summary>
    /// 出生
    /// </summary>
    public void Born(Vector3 bornPos, Vector3 baseDir, float distance, bool needGrowth)
    {
        lineRender.positionCount++;
        lineRender.SetPosition(0, bornPos);
        lastPos = bornPos;

        if(needGrowth)
        {
            Vector3 pos = bornPos;
            Vector3 endPos = pos + baseDir * distance;
            float length = distance / 10f;
            Vector3 point = pos;
            for (int i = 0; i < 10; i++)
            {
                point += baseDir * length;
                float offset = Random.Range(0, length);
                point.x += Random.Range(-offset, offset);
                point.y += Random.Range(-offset, offset);
                brithList.Add(point);
            }
            brithList.Add(endPos);

            CalcultateGrowthDir(brithList[0]);
            brithList.RemoveAt(0);
            curStatu = BranchStatu.Birth;
            isGrowthing = true;
        }
        else
        {
            curStatu = BranchStatu.Normal;
        }
    }

    /// <summary>
    /// 跟随操作
    /// </summary>
    private void UpdateOperate()
    {
        if(curStatu != BranchStatu.Normal)
        {
            return;
        }
        if(Input.GetMouseButton(0))
        {
            CalcultateGrowthDir(mainCamera.ScreenToWorldPoint(Input.mousePosition));
            isGrowthing = (lastPos - curTgtPos).sqrMagnitude > stopDistance * stopDistance;
        }
        else
        {
            isGrowthing = false;
        }
    }

    /// <summary>
    /// 朝一个方向生长
    /// </summary>
    /// <param name="dir"></param>
    private void UpdateGrowth()
    {
        if(curStatu > BranchStatu.Normal)
        {
            isGrowthing = false;
            return;
        }

        if(curStatu == BranchStatu.Birth)
        {
            if((lastPos - curTgtPos).sqrMagnitude <= stopDistance * stopDistance)
            {
                if(brithList.Count == 0)
                {
                    curStatu = BranchStatu.Normal;
                    isGrowthing = false;
                }
                else
                {
                    CalcultateGrowthDir(brithList[0]);
                    brithList.RemoveAt(0);
                }

            }
        }
        
        if(isGrowthing)
        {
            growthTimer += Time.deltaTime;
            if(growthTimer > growthRate)
            {          
                lastPos += growthDir * growthScale;
                lineRender.positionCount++;
                lineRender.SetPosition(lineRender.positionCount - 1, lastPos);
                growthTimer = 0f;
            }
        }
    }

    /// <summary>
    /// 更行碰撞器位置
    /// </summary>
    private void UpdateColliderPos()
    {
        trigger.transform.position = lastPos;
    }

    /// <summary>
    /// 创建其他
    /// </summary>
    private void CheckCreateOther()
    {
        if(curStatu == BranchStatu.CanCreateOther)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Vector3 clickPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                clickPos.z = 0f;
                for (int i = 0; i < lineRender.positionCount-5; i++)
                {
                    Vector3 pos = lineRender.GetPosition(i);
                    if((pos - clickPos).sqrMagnitude < stopDistance * stopDistance)
                    {
                        float dirX = Random.Range(-1,2);
                        float dirY = Random.Range(-1, 0);
                        RootManager.Inst.CreateNewBranch(pos, new Vector3(dirX, dirY, 0f), false);
                        curStatu = BranchStatu.Static;
                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 碰到东西
    /// </summary>
    private void OnOtherEnter(SceneObjectBase sceneObject, RootBranch branch) 
    {
        if(!branch.Equals(this))
        {
            return;
        }
        //资源
        if(sceneObject.ObjectType < ObjectType.AirWall)
        {
            SceneAsset asset = (SceneAsset)sceneObject;
            if(!asset.IsGet)
            {
                asset.Collected(); // 出发收集的资源状态
                curStatu = BranchStatu.CanCreateOther;//标记状态为可创建分支
                trigger.gameObject.SetActive(false);
                Messenger<int>.Broadcast(MessengerEventType.DATA_CHANGE_SCORE, asset.Score);//加积分
            }
        }
        //空气墙

        //虫子

        //水源游戏结束
        
    }
}
