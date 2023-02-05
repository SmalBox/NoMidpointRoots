using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景资源
/// 石头待定 暂时配置分数为0浪费一根树枝
/// </summary>
public class SceneAsset : SceneObjectBase
{
    [SerializeField] int score = 0;

    private bool isGet = false;

    protected override void OnBranchEnter(RootBranch branch)
    {
        //TODO 放特效？？？
        isGet = true;
    }
        
    public int Score { get => score; }
    public bool IsGet { get => isGet; }

    public void Collected()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            spriteRenderer.color = Color.black;
        Animator animator = GetComponent<Animator>();
        if (animator != null)
            animator.speed = 0;
    }
}
