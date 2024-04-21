using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    private Vector3 dir;
    private Rigidbody rb;
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
        rb = GetComponent<Rigidbody>();
        dir = this.gameObject.transform.forward;
    }

    protected virtual void Update()
    {
        Move();
       
    }

    private void Move()  //子弹的移动
    {
        this.transform.Translate(dir * Time.deltaTime * Speed);
        
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        // 检查碰撞到的是不是墙体
        if (other.CompareTag("wall"))
        {
            print("碰到墙体");
            // 获取碰撞点的法线，用于计算反射方向
            Vector3 normal = other.ClosestPoint(transform.position + rb.velocity * Time.deltaTime) - transform.position;
            normal = normal.normalized;

            // 计算反射方向
            Vector3 reflectedDirection = Vector3.Reflect(rb.velocity, normal);
            reflectedDirection.y = 0f; // 保持垂直方向速度不变

            // 更新子弹的速度
            rb.velocity = reflectedDirection.normalized * Speed;
        }
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
