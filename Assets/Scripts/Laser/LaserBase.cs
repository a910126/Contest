using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBase : MonoBehaviour
{
    /// <summary>
    /// 释放前摇
    /// </summary>
    protected float AtkForward;

    /// <summary>
    /// 持续时间
    /// </summary>
    protected float Duration;

    /// <summary>
    /// 自身的名字
    /// </summary>
    protected string Name;

    protected virtual void Awake()
    {
        Name = this.gameObject.name;
    }


    public void Destroy()  //销毁激光
    {
        StartCoroutine(DestoryLaser());
    }

    private IEnumerator DestoryLaser()
    {
        yield return new WaitForSeconds(Duration);
        PoolMgr.GetInstance().PushObj(Name,this.gameObject);
    }
}
