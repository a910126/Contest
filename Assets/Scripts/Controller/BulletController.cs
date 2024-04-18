using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening; // 引入DoTween库

public class BulletController : MonoBehaviour
{
    public float speed = 15f; // 子弹的速度
    private float timer = 0;


    public int damage = 20; // 子弹的伤害值
    void OnEnable()
    {
        timer = 0; // 每次子弹被激活时，重置计时器
    }

    void Update()
    {
        // 更新子弹的位置
        transform.position += transform.forward * speed * Time.deltaTime;

        timer += Time.deltaTime; // 计时器增加
        if (timer >= 5) // 如果子弹被取出5秒后
        {
            timer = 0; // 重置计时器
            PoolMgr.GetInstance().PushObj("Prefabs/Bullets/Bullet", gameObject); // 将子弹回收到对象池中
        }
    }


    void OnTriggerEnter(Collider other)
    {
        
    }
}
