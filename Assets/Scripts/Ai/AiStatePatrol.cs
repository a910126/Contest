using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStatePatrol : AiStateBase
{
    /// <summary>
    /// 记录巡逻点
    /// </summary>
    private int temp=0;
    public AiStatePatrol(AiLogic logic) : base(logic)
    {

    }

    public override void EnterAiState()
    {
        Debug.Log("进入巡逻");
        logic.monster.Move(logic.monster.BornPos, logic.monster.Patrols[temp]);
    }

    public override void ExitAiState()
    {
        logic.monster.StopMove();
        Debug.Log("退出巡逻状态，进入追击状态");
    }

    public override void UpdateAiState()
    {
        Dis(ref temp);
        FindPlayer();
    }

    private void Dis(ref int temp)  //巡逻逻辑
    {
        float dis = Vector3.Distance(this.logic.monster.transform.position, this.logic.monster.Patrols[temp]);
        if (dis<=0.08f)  //巡逻没有转弯一直走跟这个误差有关（改大就会减少出现）
        {
            temp++;
            if (temp > 3)
                temp = 0;
            logic.monster.Move(logic.monster.Patrols[temp - 1 < 0 ? 3 : temp - 1], logic.monster.Patrols[temp]);
        }   
    }

    private void FindPlayer()  //发现玩家
    {
        float temp = Vector3.Distance(logic.monster.transform.position,logic.monster.PlayerPos);
        //Debug.Log(logic.monster.MonsterRange+"范围");
        //Debug.Log(temp + "距离");
        if (temp <= logic.monster.MonsterRange)
        {
            logic.ChangeState(E_State.MOVE);
        }
    }

}
