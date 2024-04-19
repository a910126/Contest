using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    /// <summary>
    /// 子弹的速度
    /// </summary>
    protected float Speed;

    /// <summary>
    /// 子弹的伤害
    /// </summary>
    public float Damage;

    /// <summary>
    /// 子弹销毁时间
    /// </summary>
    protected float DestoryTime;

    /// <summary>
    /// 子弹自己的名字
    /// </summary>
    protected string Name;

    protected virtual void Awake()
    {
        Name = this.gameObject.name;
    }

    protected virtual void Update()
    {
        Move();
    }

    private void Move()  //子弹的移动
    {
        this.transform.Translate(this.gameObject.transform.forward * Time.deltaTime * Speed);
    }
    public void Destory()  //子弹的销毁
    {
        StartCoroutine(DestoryBullet());
    }

    private IEnumerator DestoryBullet()
    {
        yield return new WaitForSeconds(DestoryTime);
        PoolMgr.GetInstance().PushObj(name, this.gameObject);
    }
}
