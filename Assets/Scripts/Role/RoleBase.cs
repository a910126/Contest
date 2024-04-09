using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoleBase :MonoBehaviour  //所有人物的基类
{
    /// <summary>
    /// 血量
    /// </summary>
    protected int Hp;

    /// <summary>
    /// 防御力
    /// </summary>
    protected int Def;

    /// <summary>
    /// 移动速度
    /// </summary>
    protected float Movespeed;

    /// <summary>
    /// 转动速度
    /// </summary>
    protected float Rotatespeed;

    /// <summary>
    /// 人物移动
    /// </summary>
    protected abstract void Move();

    /// <summary>
    /// 人物死亡
    /// </summary>
    public virtual void Dead()
    {

    }

    /// <summary>
    /// 人物受伤
    /// </summary>
    public abstract void Hurt();

    /// <summary>
    /// 人物攻击
    /// </summary>
    public abstract void Atk();
}
