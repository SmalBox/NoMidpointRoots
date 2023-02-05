using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : SingletonMonoClass<SoundMgr>
{
    public AudioSource audioSource;
    
    public enum SoundType
    {
        Click,
        Growth,
        GetEnergy,
        HitStone,
        HitWorm,
        Succeed,
        Fail,
    }

    public void PlaySound(SoundType type)
    {
        switch (type)
        {
            case SoundType.Click:
                audioSource.clip = Resources.Load("Sound/点击") as AudioClip;
                audioSource.Play();
                break;
            case SoundType.Growth:
                audioSource.clip = Resources.Load("Sound/生长") as AudioClip;
                audioSource.Play();
                break;
            case SoundType.GetEnergy:
                audioSource.clip = Resources.Load("Sound/生长") as AudioClip;
                audioSource.Play();
                break;
            case SoundType.HitStone:
                audioSource.clip = Resources.Load("Sound/碰到石头") as AudioClip;
                audioSource.Play();
                break;
            case SoundType.HitWorm:
                audioSource.clip = Resources.Load("Sound/碰到虫子") as AudioClip;
                audioSource.Play();
                break;
            case SoundType.Succeed:
                audioSource.clip = Resources.Load("Sound/关卡成功") as AudioClip;
                audioSource.Play();
                break;
            case SoundType.Fail:
                audioSource.clip = Resources.Load("Sound/关卡失败") as AudioClip;
                audioSource.Play();
                break;
        }
    }
}
