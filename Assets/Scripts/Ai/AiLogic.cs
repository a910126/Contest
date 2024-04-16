using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态的枚举
/// </summary>
public enum E_State
{
    NULL,
    /// <summary>
    /// 巡逻状态
    /// </summary>
    PATROL,
    /// <summary>
    /// 移动状态
    /// </summary>
    MOVE,
    /// <summary>
    /// 攻击状态
    /// </summary>
    ATK,
    /// <summary>
    /// 出范围返回出生点
    /// </summary>
    BACK,
}
public class AiLogic 
{
    public RoleBase monster;

    public E_State nowstate=E_State.NULL;

    //利用字典存储状态的逻辑，方便Ai逻辑的动态添加
    //用枚举作为键，用Ai逻辑基类作为值，父类装子类
    private Dictionary<E_State,AiStateBase> stateDic=new Dictionary<E_State,AiStateBase>();

    public AiStateBase nowAistate;  //当前Ai逻辑
    public AiLogic(RoleBase monster) 
    {
        this.monster = monster;
        stateDic.Add(E_State.PATROL, new AiStatePatrol(this));
        stateDic.Add(E_State.MOVE, new AiStateMove(this));
        stateDic.Add(E_State.ATK, new AiStateAtk(this));
        stateDic.Add(E_State.BACK, new AiStateBack(this));
        ChangeState(E_State.PATROL);
    }
    public void UpdateState()  //循环状态
    {
        if (monster.IsDead) return;
        nowAistate.UpdateAiState();
    }    
    public void ChangeState(E_State state)  //改变状态
    {        
        //先执行上一次状态的结束逻辑
        if(nowstate!=E_State.NULL)
        stateDic[nowstate].ExitAiState();
        //再改变状态 
        nowstate=state;
        //再执行最新状态的开始逻辑
        stateDic[nowstate].EnterAiState();
        nowAistate = stateDic[nowstate];
    }
}
