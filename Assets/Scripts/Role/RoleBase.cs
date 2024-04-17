﻿using System.Collections;
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
    /// 怪物的视野范围
    /// </summary>
    public float MonsterRange;

    /// <summary>
    /// 距离Player多少去攻击
    /// </summary>
    public float DisToAtk;

    /// <summary>
    /// 距离BornPos多少回BornPos
    /// </summary>
    public float DisToBack;

    /// <summary>
    /// 是否死亡
    /// </summary>
    public bool IsDead;

    /// <summary>
    /// 移动方向
    /// </summary>
    protected Vector3 MoveDic;

    /// <summary>
    /// 玩家的位置
    /// </summary>
    public Vector3 PlayerPos;

    /// <summary>
    /// 出生点
    /// </summary>
    public Vector3 BornPos;

    /// <summary>
    /// 巡逻点1
    /// </summary>
    private Vector3 Patrol1;

    /// <summary>
    /// 巡逻点2
    /// </summary>
    private Vector3 Patrol2;

    /// <summary>
    /// 巡逻点3
    /// </summary>
    private Vector3 Patrol3;

    /// <summary>
    /// 巡逻点4
    /// </summary>
    private Vector3 Patrol4;

    /// <summary>
    /// 巡逻点的数组
    /// </summary>
    public Vector3[] Patrols;

    /// <summary>
    /// 得到自己身上的Animation脚本
    /// </summary>
    //protected Animator animatior;

    protected virtual void Awake()
    {
        //animatior = this.gameObject.GetComponent<Animator>();
        Patrol1 = BornPos + new Vector3(0,0,-10);
        Patrol2 = BornPos + new Vector3(10, 0, -10);
        Patrol3 = BornPos + new Vector3(10, 0, 10);
        Patrol4 = BornPos + new Vector3(0, 0, 10);
        //Debug.Log(Patrol1);
        Patrols = new Vector3[4] {Patrol1, Patrol2, Patrol3, Patrol4};
    }

    protected virtual void Update()
    {

        PlayerPos = GameObject.Find("Player").transform.position;
        this.gameObject.transform.Translate(MoveDic * Time.deltaTime * Movespeed);  //怪物的移动
    }


    /// <summary>
    /// 怪物的移动
    /// </summary>
    /// <param name="FirstPos"></param>
    /// <param name="SecondPos"></param>
    public virtual void Move(Vector3 FirstPos,Vector3 SecondPos)
    {
        MoveDic = (SecondPos - FirstPos).normalized;
    }

    /// <summary>
    /// 停止移动
    /// </summary>
    public virtual void StopMove()
    {
        MoveDic = Vector3.zero;
    }

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
