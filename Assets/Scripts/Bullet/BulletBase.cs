using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    
    protected Rigidbody rb;
    protected float Speed;
    public float Damage;
    protected float DestoryTime;
    protected string Name;
    public LayerMask collisionLayer;

    protected virtual void Awake()
    {
        Name = this.gameObject.name;
        rb = GetComponent<Rigidbody>();
       
    }

    protected virtual void OnEnable()
    {
        // 当子弹被激活时，重置方向和速度
        transform.forward = Vector3.forward;
        rb.velocity = Vector3.zero;
    }

    protected virtual void Update()
    {
        Move();
        // 绘制射线（仅在编辑器中可见）
        Debug.DrawRay(transform.position, transform.forward * 2f, Color.red, 2f);
    }

    protected virtual void Move()
    {
        rb.velocity = transform.forward * Speed;
    }

    private void ChangeDirection(Vector3 normal)
    {
        // 计算反弹方向
        Vector3 reflection = Vector3.Reflect(transform.forward, normal);
        // 设置子弹的方向为反弹方向
        transform.forward = reflection;
        rb.velocity = reflection * Speed; // 重新设置速度
    }

    public void Destroy()
    {
        StartCoroutine(DestroyBullet());
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(DestoryTime);
        // 在子弹被推入对象池之前，重置其状态
        ResetState();
        PoolMgr.GetInstance().PushObj(Name, this.gameObject);
    }

    private void ResetState()
    {
        // 重置子弹的方向和速度
        transform.forward = Vector3.forward;
        rb.velocity = Vector3.zero;
    }

    private RaycastHit hitInfo;
    private bool isHitWall = false;

    protected virtual void FixedUpdate()
    {
        // 在FixedUpdate中进行射线检测，以保持物理检测的一致性
        isHitWall = Physics.Raycast(transform.position, transform.forward, out hitInfo, 10f, collisionLayer);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (isHitWall && other.CompareTag("wall"))
        {
            ChangeDirection(hitInfo.normal);
        }
        else
        {
           
        }
    }
}
