using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateAtk : AiStateBase
{
    private float AtkTime=2;  //攻击间隔时间
    private float NextAtk = 0;  //下次攻击时间

    public AiStateAtk(AiLogic logic) : base(logic)
    {
    }

    public override void EnterAiState()
    {
        Debug.Log("进入攻击状态");
    }

    public override void ExitAiState()
    {
        
    }

    public override void UpdateAiState()
    {
        if (Time.time >= NextAtk)
        {
            logic.monster.Atk();
            NextAtk = Time.time + AtkTime;
        }
        
    }


}
