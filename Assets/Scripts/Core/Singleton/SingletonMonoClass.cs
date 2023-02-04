using UnityEngine;

public class SingletonMonoClass<T> : MonoBehaviour where T : SingletonMonoClass<T>
{
    public static T Ins = null;

    protected void Awake()
    {
        Ins = this as T;
    }
}
