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
        
    }

    public override void UpdateAiState()
    {
        //logic.monster.Move(logic.monster.BornPos, logic.monster.Patrol1);
        Dis(ref temp);
    }

    private void Dis(ref int temp)
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

}
