using System.Collections;
using System.Collections.Generic;
using Auto.Logic.Core.Message;
using UnityEngine;

/// <summary>
/// 场景物体基类
/// </summary>
public abstract class SceneObjectBase : MonoBehaviour
{
    [SerializeField] ObjectType objectType = ObjectType.K;

    private void OnTriggerEnter(Collider other)
    {
        var branch = other.GetComponentInParent<RootBranch>();
        if(branch != null)
        {
            Messenger<SceneObjectBase, RootBranch>.Broadcast(MessengerEventType.ENTER_OBJECT, this, branch);

            OnBranchEnter(branch);
        }
    }

    protected abstract void OnBranchEnter(RootBranch branch);

    public ObjectType ObjectType { get=>objectType; }
}

/// <summary>
/// 物体类型
/// </summary>
public enum ObjectType
{
    K,
    N,
    P,
    Stone,
    Water,
    Worms,
    AirWall
}
